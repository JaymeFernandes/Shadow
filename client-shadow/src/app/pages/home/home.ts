
import { Component } from '@angular/core';
import { IPost } from '../../shared/interfaces/IPost';
import { Post } from './components/post/post';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [ Post ],
  templateUrl: './home.html',
  styleUrl: './home.scss'
})
export class Home {
  readonly posts: IPost[] = [
  {
    manga: {
      title: 'Demon Slayer',
      author: 'Koyoharu Gotouge',
      coverImage: 'https://doublesama.com/wp-content/uploads/2019/11/Demon-Slayer-cover.jpg',
      rating: 4.5
    },
    author: {
      name: 'Idzmon',
      avatar: 'https://media.tenor.com/1uKpKHCxNsEAAAAi/anime-joget-rgb.gif',
      title: 'Melhor anime do mundo',
      description: `

![Demon Slayer](https://media1.tenor.com/m/gMQg8Zqy0bgAAAAC/demon-slayer-tanjiro.gif)

## Sobre Demon Slayer

**Demon Slayer** é um anime incrível com uma animação de tirar o fôlego e uma história envolvente.
O protagonista, **Tanjiro Kamado**, luta para salvar sua irmã **Nezuko**, que foi transformada em demônio.

---

### 🗡️ Personagens Principais
- **Tanjiro Kamado** — Protagonista determinado.
- **Nezuko Kamado** — Sua irmã demoníaca.
- **Zenitsu Agatsuma** — Medroso, mas poderoso.
- **Inosuke Hashibira** — Selvagem e imprevisível.

---

### ⭐ Motivos para assistir:
1. Animação impecável
2. Trilha sonora emocionante
3. Batalhas épicas
4. História cativante

> "Se você ama shounen, **Demon Slayer** é obrigatório!"
      `,
    },
    likes: 3000,
    userHasLiked: false,
    userHasFavorited: false,
    comments: []
  },
  {
    manga: {
        title: 'Dragon Ball Super',
        author: 'Toriyama, Akira',
        coverImage: 'https://http2.mlstatic.com/D_NQ_NP_991284-MLU77228825470_072024-O.webp',
        rating: 4.8
      },
      author: {
        name: 'Matheus',
        avatar: 'https://media.tenor.com/VlcpKOUOq8gAAAAj/one-piece-nico-robin.gif',
        title: 'Dragon Ball: O Despertar do SSJ4 🐉',
        description:
`
![Dragon Ball](https://static.tumblr.com/d7f9cfbbfe0e3e752f8f9511edee9f89/zcqhzp1/oU8phbkol/tumblr_static_tumblr_static_-689028515-content_focused_v3.gif)

---

**Dragon Ball** é uma franquia de mídia japonesa criada por \`Akira Toriyama\`, centrada nas aventuras de \`Son Goku\`.
Um menino com rabo de macaco que busca as esferas do dragão, capazes de realizar qualquer desejo. A história, que começa com um tom mais cômico e focado em artes marciais, evolui para batalhas épicas e a defesa da Terra contra diversas ameaças. A franquia inclui mangás, animes, filmes, jogos e outros produtos.

- \`Dragon Ball\`
- \`Dragon Ball Z\`
- \`Dragon Ball GT\`
- \`Dragon Ball Super\`

`,
    },
    likes: 9999,
    userHasLiked: false,
    userHasFavorited: false,
    comments: []
  },
  {
    manga: {
        title: 'Naruto',
        author: 'Masashi Kishimoto',
        coverImage: 'https://i.pinimg.com/736x/a8/91/d9/a891d9ddd8472c3fd84bf85a80693ac9.jpg',
        rating: 4.8
      },
      author: {
        name: 'IDzmon',
        avatar: 'https://media.tenor.com/1uKpKHCxNsEAAAAi/anime-joget-rgb.gif',
        title: '🟧 Naruto',
        description:
`
# Sobre Naruto

## Introdução

**Naruto** é uma obra criada por *Masashi Kishimoto*, que marcou gerações com sua história sobre superação, amizade e sonhos.

> "Eu nunca volto atrás na minha palavra... esse é o meu jeito ninja!" — *Naruto Uzumaki*

---

## Enredo

A série conta a história de **Naruto Uzumaki**, um jovem órfão que sonha em se tornar *Hokage*, o líder da Vila da Folha, para ser reconhecido por todos.

---

## Vilas Ninjas

- **Konoha (Vila da Folha)** 🌳
- **Suna (Vila da Areia)** 🌪️
- **Kiri (Vila da Névoa)** 🌫️
- **Kumo (Vila da Nuvem)** ☁️
- **Iwa (Vila da Pedra)** 🪨

---

## Personagens Principais

1. **Naruto Uzumaki** — O ninja loiro mais teimoso.
2. **Sasuke Uchiha** — Sobrevivente do clã Uchiha, busca vingança.
3. **Sakura Haruno** — Médica ninja e super forte.
4. **Kakashi Hatake** — O mestre copiador.

---

## Jutsus Icônicos

- **Rasengan:** Esfera de chakra em rotação.
- **Chidori:** Golpe elétrico concentrado.
- **Kage Bunshin no Jutsu:** Técnica dos clones das sombras.
- **Amaterasu:** Chamas negras que nunca se a

## Código Ninja

\`\`\`ts
function rasengan() {
  return "🌀 Um poderoso orbe de chakra concentrado!";
}
\`\`\`

`,
    },
    likes: 9999,
    userHasLiked: false,
    userHasFavorited: false,
    comments: []
  },
  {
    manga: {
        title: 'Bleach',
        author: 'Noriaki Kubo',
        coverImage: 'https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1603200920i/55745579.jpg',
        rating: 5.0
      },
      author: {
        name: 'IDzmon',
        avatar: 'https://media.tenor.com/1uKpKHCxNsEAAAAi/anime-joget-rgb.gif',
        title: 'Bleach ⚔️',
        description:
`
# Sobre Bleach

## Introdução

**Bleach** é um anime e mangá criado por *Tite Kubo*, conhecido por sua estética estilosa, batalhas intensas e personagens carismáticos.

> "Mesmo que eu possa não ter poder agora... eu sempre terei uma espada invisível!" — *Ichigo Kurosaki*

---

## Enredo

A história segue **Ichigo Kurosaki**, um jovem que obtém os poderes de um *Shinigami* (Deus da Morte) e precisa proteger os vivos dos *Hollows* — espíritos malignos — enquanto ajuda almas perdidas a encontrarem a paz.

## Sociedades Importantes

- **Sociedade das Almas**
  - Local onde os *Shinigamis* vivem.
- **Hueco Mundo**
  - Mundo dos *Hollows* e dos *Arrancars*.
- **Mundo Real**
  - Onde Ichigo e seus amigos vivem.

---

## Personagens Principais

1. **Ichigo Kurosaki** — Protagonista, meio humano, meio Shinigami, meio tudo.
2. **Rukia Kuchiki** — Shinigami que dá seus poderes a Ichigo.
3. **Uryuu Ishida** — Quincy, arqueiro espiritual.
4. **Orihime Inoue** — Tem poderes de rejeitar eventos, quase como "reverter o tempo".
5. **Yasutora 'Chad' Sado** — Força bruta com braços espirituais.

---

## Habilidades e Técnicas

- **Bankai:** Forma avançada da Zanpakutou.
- **Getsuga Tenshou:** Golpe icônico de Ichigo.
- **Cero:** Disparo de energia dos *Arrancars*.
- **Gran Rey Cero:** Versão ultra do Cero.
`,
    },
    likes: 9999,
    userHasLiked: false,
    userHasFavorited: false,
    comments: []
  }

];

}
