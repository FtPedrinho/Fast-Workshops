# Fast Workshops API

API REST desenvolvida em ASP.NET Core para o gerenciamento de workshops e o rastreamento da participação de colaboradores.

O projeto foi desenvolvido como solução para o desafio de Rastreamento de Participação em Workshops da FAST Soluções.

## Funcionalidades

- Cadastro, consulta, atualização e exclusão de workshops;
- Cadastro, consulta, atualização e exclusão de colaboradores;
- Registro de participação de colaboradores em workshops;
- Consulta das participações de um workshop;
- Validação de relacionamentos entre colaboradores, workshops e participações;
- Prevenção de participação duplicada no mesmo workshop;
- Persistência dos dados utilizando Entity Framework Core e SQL Server;
- Documentação e testes da API.

## Tecnologias

- .NET 8
- ASP.NET Core
- Entity Framework Core
- SQL Server
- xUnit
- Swagger / OpenAPI
- Git

## Arquitetura
```plaintext
Fast-Workshops/
├── backend/
│   └── WorkshopApi/
│       ├── Controllers/
│       │   ├── WorkshopsController.cs
│       │   ├── ColaboradoresController.cs
│       ├── Services/
│       │   ├── WorkshopService.cs
│       │   ├── ColaboradorService.cs
│       ├── Repositories/
│       │   ├── WorkshopRepository.cs
│       │   ├── ColaboradorRepository.cs
│       │   └── ParticipacaoRepository.cs
│       ├── Models/
│       │   ├── WorkshopModel.cs
│       │   ├── ColaboradorModel.cs
│       │   └── ParticipacaoModel.cs
│       ├── DTOs/
│       │   ├── WorkshopDto.cs
│       │   ├── ColaboradorDto.cs
│       │   └── ParticipacaoDto.cs
│       ├── Database/
│       │   └── AppDbContext.cs
│       ├── Program.cs
│       └── appsettings.json
├── frontend/
│   src/
|   ├── components/
|   │   ├── Navbar.js
|   │   ├── modal.js
|   │   ├── confirmModal.js
|   │   ├── CollaboratorCard.js
|   │   └── WorkshopCard.js
|   ├── pages/
|   │   ├── colaboradores.js
|   │   ├── workshops.js
|   │   ├── dashboard.js
|   │   └── workshopDetalhes.js
|   ├── services/
|   │   └── api.js
|   ├── mocks/
|   │   └── data.js
|   ├── main.js
|   └── style.css
└── README.md
```
