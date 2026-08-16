# Fast-Workshops
API REST e aplicação web para gerenciamento e análise de participação em workshops.

## Arquitetura
```plaintext
Fast-Workshops/
├── backend/
│   └── WorkshopApi/
│       ├── Controllers/
│       │   ├── WorkshopsController.cs
│       │   ├── ColaboradoresController.cs
│       │   └── PresencasController.cs
│       ├── Services/
│       │   ├── WorkshopService.cs
│       │   ├── ColaboradorService.cs
│       │   └── PresencaService.cs
│       ├── Repositories/
│       │   ├── WorkshopRepository.cs
│       │   ├── ColaboradorRepository.cs
│       │   └── PresencaRepository.cs
│       ├── Models/
│       │   ├── Workshop.cs
│       │   ├── Colaborador.cs
│       │   └── Presenca.cs
│       ├── DTOs/
│       │   ├── WorkshopDto.cs
│       │   ├── ColaboradorDto.cs
│       │   └── PresencaDto.cs
│       ├── Database/
│       │   └── AppDbContext.cs
│       ├── Program.cs
│       └── appsettings.json
├── frontend/
│   └── src/
│       ├── components/
│       ├── pages/
│       ├── services/
│       │   └── api.js
│       ├── App.jsx
│       └── main.jsx
└── README.md
```
