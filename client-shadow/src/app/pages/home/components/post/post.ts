import { afterNextRender, Component, Input, OnInit, signal } from '@angular/core';
import { IPost } from '../../../../shared/interfaces/Posts/IPost';
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

      marked.setOptions({
        gfm: true,
        breaks: true
      });

      console.log(this.post.manga?.title)

      const rawHtml = marked.parse(this.post.description);

      const limitedHtml = this.limitImagesInHtml(rawHtml.toString(), 4);

      const safeHtml = DOMPurify.sanitize(limitedHtml, {
        ALLOWED_TAGS: [
          'p', 'br', 'strong', 'em', 'b', 'i', 'u', 'del', 's',
          'blockquote', 'ul', 'ol', 'li', 'code', 'pre',
          'h1', 'h2', 'h3', 'h4', 'h5', 'h6',
          'hr', 'a', 'img'
        ],
        ALLOWED_ATTR: ['href', 'src', 'alt', 'title', 'rel', 'target'],
        FORBID_TAGS: ['video', 'svg', 'iframe', 'script', 'style', 'object', 'embed', 'link', 'meta', 'base'],
        FORBID_ATTR: ['onerror', 'onload', 'onclick', 'onmouseover', 'class', 'style']
      });

      const styledHtml = safeHtml
        .replace(/<img\b/g, `<img class="w-full max-w-full max-h-[400px] rounded-xl mb-4 object-contain h-auto"`)
        .replace(/<a\b/g, `<a class="text-indigo-400 hover:text-indigo-300 break-words"`)
        .replace(/<h1\b/g, `<h1 class="font-bold text-2xl sm:text-3xl mt-4 mb-2"`)
        .replace(/<h2\b/g, `<h2 class="font-semibold text-xl sm:text-2xl mt-4 mb-2"`)
        .replace(/<h3\b/g, `<h3 class="font-semibold text-lg sm:text-xl mt-3 mb-1.5"`)
        .replace(/<p\b/g, `<p class="text-base leading-relaxed mb-3 break-words"`)
        .replace(/<ul\b/g, `<ul class="list-disc pl-6 mb-3 break-words"`)
        .replace(/<ol\b/g, `<ol class="list-decimal pl-6 mb-3 break-words"`)
        .replace(/<li\b/g, `<li class="mb-1 break-words"`)
        .replace(/<strong\b/g, `<strong class="font-semibold text-white"`)
        .replace(/<em\b/g, `<em class="italic text-gray-300"`)
        .replace(/<blockquote\b/g, `<blockquote class="border-l-4 border-gray-600 pl-4 italic text-gray-400 mb-3"`)
        .replace(/<code\b/g, `<code class="bg-gray-800 text-gray-100 px-1.5 py-0.5 rounded text-sm whitespace-pre-wrap"`)
        .replace(/<pre\b/g, `<pre class="bg-gray-900 p-4 rounded overflow-x-auto mb-4"`)
        .replace(/<hr\s*\/?>/g, `<hr class="border-gray-700 my-6" />`);

      const cleanedHtml = styledHtml.replace(/(<br\s*\/?>\s*){2,}/g, '<br/>');

      this.sanitizedHtml.set(this.sanitizer.bypassSecurityTrustHtml(cleanedHtml));

    })
  }

  limitImagesInHtml(html: string, maxImages: number = 5): string {
    const imgRegex = /([ \t\r\n]*)<img\b[^>]*>([ \t\r\n]*)/gi;
    const images = [...html.matchAll(/<img\b[^>]*>/gi)];

    if (images.length <= maxImages) return html;

    let count = 0;
    return html.replace(imgRegex, (match) => {
      count++;
      return count <= maxImages ? match : '';
    });
  }


}
