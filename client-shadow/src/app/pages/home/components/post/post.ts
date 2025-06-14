import { afterNextRender, Component, Input, OnInit, signal } from '@angular/core';
import { IPost } from '../../../../shared/interfaces/IPost';
import { LucideAngularModule, MessageCircle, Heart, Share, Star } from 'lucide-angular';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { marked } from 'marked';
import DOMPurify from 'dompurify';


@Component({
  selector: 'app-post',
  standalone: true,
  imports: [ LucideAngularModule ],
  templateUrl: './post.html',
  styleUrl: './post.scss'
})
export class Post {
  @Input({ required: true }) post! : IPost;
  sanitizedHtml = signal<SafeHtml>('');

  readonly MessageCircle = MessageCircle;
  readonly Heart = Heart;
  readonly Share = Share;
  readonly Star = Star;

  constructor(private sanitizer: DomSanitizer)
  {
    afterNextRender(() => {
      marked.setOptions({ gfm: true, breaks: true });
      const rawHtml = marked.parse(this.post.author.description);


      const cleanHtml = DOMPurify.sanitize(
        rawHtml.toString()
          // Links
          .replace(/<a\b/g, `<a class="text-indigo-400 hover:text-indigo-300"`)

          // Headers
          .replace(/<h1\b/g, `<h1 class="font-bold text-3xl mt-4 mb-2"`)
          .replace(/<h2\b/g, `<h2 class="font-semibold text-2xl mt-4 mb-2"`)
          .replace(/<h3\b/g, `<h3 class="font-semibold text-xl mt-3 mb-1.5"`)

          // Parágrafos
          .replace(/<p\b/g, `<p class="text-base leading-relaxed mb-3"`)

          // Listas
          .replace(/<ul\b/g, `<ul class="list-disc pl-6 mb-3"`)
          .replace(/<ol\b/g, `<ol class="list-decimal pl-6 mb-3"`)
          .replace(/<li\b/g, `<li class="mb-1"`)

          // Ênfase e Negrito
          .replace(/<strong\b/g, `<strong class="font-semibold text-white"`)
          .replace(/<em\b/g, `<em class="italic text-gray-300"`)

          // Citações
          .replace(/<blockquote\b/g, `<blockquote class="border-l-4 border-gray-600 pl-4 italic text-gray-400 mb-3"`)

          // Código inline e bloco
          .replace(/<code\b/g, `<code class="bg-gray-800 text-gray-100 px-1.5 py-0.5 rounded text-sm"`)
          .replace(/<pre\b/g, `<pre class="bg-gray-900 p-4 rounded overflow-x-auto mb-4"`)

          // Linha horizontal
          .replace(/<hr\s*\/?>/g, `<hr class="border-gray-700 my-6" />`)
      );



      this.sanitizedHtml.set(this.sanitizer.bypassSecurityTrustHtml(cleanHtml));
    })
  }
}
