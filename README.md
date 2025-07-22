# AniConnect — Rede Social de Animes (Open Source)

AniConnect é uma rede social open source voltada para fãs de animes, desenvolvida como projeto pessoal para estudos, aprimoramento profissional e hobby.

> 🚧 Projeto em desenvolvimento contínuo.

A aplicação é pensada para ser moderna, escalável e amigável tanto para usuários quanto para desenvolvedores.

---

## ✨ Funcionalidades (Parciais/Em desenvolvimento)

- Autenticação com OpenID Connect
- Cadastro e login de usuários
- Feed social de postagens sobre animes
- Curtidas, comentários e seguidores
- Upload de imagens com AWS S3
- Busca rápida de conteúdos com Meilisearch *(em desenvolvimento)*
- Renderização SSR (Angular Universal)
- Documentação com Swagger
- Perfil do usuário e histórico de interações
- SEO otimizado com SSR

---

## 💻 Tecnologias Utilizadas

### 🧠 Back-end — ASP.NET Core

| Tecnologia | Descrição |
|-----------|-----------|
| **ASP.NET Core** | Framework principal para a API |
| **Entity Framework Core** | ORM para banco relacional (PostgreSQL) |
| **EFCore.BulkExtensions** | Extensão para operações em massa |
| **System.Linq.Async** | Consultas assíncronas com LINQ |
| **OpenIddict** | Autenticação e autorização via OpenID Connect |
| **Microsoft Identity** | Gerenciamento de usuários e roles |
| **Hallang ProblemDetails** | Padrão de respostas para erros da API |
| **Amazon S3 SDK (AWSSDK.S3)** | Armazenamento de arquivos |
| **Swagger / Swashbuckle** | Geração de documentação interativa |
| **SixLabors.ImageSharp** | Manipulação de imagens |
| **PostgreSQL** | Banco de dados relacional |
| **MongoDB** | Banco NoSQL para dados não estruturados |
| **Meilisearch** *(em desenvolvimento)* | Busca rápida e full-text |

---

### 🎨 Front-end — Angular

| Tecnologia | Descrição |
|-----------|-----------|
| **Angular** | Framework SPA para interface |
| **Angular Universal** | Suporte a SSR (Server-Side Rendering) |
| **Tailwind CSS** | Utilitário de estilo moderno e responsivo |

---

## 🚀 Como Rodar Localmente
### Pré-requisitos
- .NET 8 SDK ou superior
- Node.js 18+
- Angular CLI
- PostgreSQL
- MongoDB
- Meilisearch *(opcional por enquanto)*

### Backend

```bash
cd AniConnect.Api
dotnet restore
dotnet ef database update
dotnet run
```

### Frontend
```bash
cd AniConnect.Frontend
npm install
npm run dev:ssr
```


## 🤝 Contribuições

Contribuições são bem-vindas! Mesmo sendo um projeto educacional, sugestões e melhorias são muito bem-vindas.

1. Fork o projeto
2. Crie sua branch (`git checkout -b feature/nova-feature`)
3. Commit suas mudanças (`git commit -m 'feat: minha nova feature'`)
4. Push para a branch (`git push origin feature/nova-feature`)
5. Abra um Pull Request

## 📜 Licença
[MIT]()

## ❗ Aviso legal:

Este projeto foi desenvolvido exclusivamente para fins educacionais e de estudo.  
O autor não se responsabiliza por qualquer uso indevido, ilegal ou não autorizado desta aplicação,  
incluindo mas não se limitando à violação de direitos autorais, distribuição indevida de conteúdo, ou qualquer outra atividade que infrinja leis locais ou internacionais.
O uso é por sua conta e risco.
