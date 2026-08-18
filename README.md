# FAST Workshops

Aplicação web para gerenciamento e análise da participação de colaboradores nos workshops trimestrais da FAST Soluções.

O projeto é dividido em:

- Backend: API REST desenvolvida em C# com ASP.NET Core.
- Frontend: aplicação web desenvolvida em JavaScript com Vite.
- Banco de dados: SQL Server.
- Documentação da API: Swagger/OpenAPI.
- Gráficos: Chart.js.

### Tecnologias utilizadas

#### Backend
- C#
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI
#### Frontend
- JavaScript
- HTML5
- CSS3
- Vite
- Chart.js

### Arquitetura
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
### Pré-requisitos

#### Instale:

.NET 8 SDK
Node.js
SQL Server
EF Core CLI (dotnet-ef)

#### Clone o projeto

````plaintext
git clone <URL_DO_REPOSITORIO>
cd Fast-Workshops
````

#### Configure o banco

No arquivo: _backend/WorkshopApi/appsettings.json_

configure a DefaultConnection para o SQL Server da máquina.

#### Inicie o Backend

Em um terminal do VS Code:

````plaintext
cd backend/WorkshopApi
dotnet restore
dotnet ef database update
$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet run
````

A API ficará disponível em: _http://localhost:5000_

Swagger: _http://localhost:5000/swagger_

#### Inicie o Frontend

Abra outro terminal:

````plaintext
cd frontend
npm install
npm run dev
````

Abra no navegador o endereço mostrado pelo Vite, normalmente: _http://localhost:5173_

Importante

O backend e o frontend precisam estar rodando ao mesmo tempo.

Frontend → http://localhost:5173
              
Backend  → http://localhost:5000
              
Se a API estiver usando outra porta, altere a API_BASE_URL em: _frontend/src/services/api.js_

### Bibliotecas e dependências

#### Backend — C# / .NET 8

O backend utiliza:

Microsoft.EntityFrameworkCore.SqlServer 8.0.19 — integração com SQL Server.
Microsoft.EntityFrameworkCore.Design 8.0.19 — suporte a migrations e ferramentas do Entity Framework Core.
SQL Server 2022 Developer (16.0.1000.6)
Swashbuckle.AspNetCore 6.6.2 — documentação e interface do Swagger.

Essas dependências são restauradas automaticamente com:

```` plaintext
dotnet restore
````

#### Frontend - JavaScript

O frontend utiliza:

Vite 8.2.0 — servidor de desenvolvimento e build.
Chart.js 4.5.1 — criação dos gráficos de participação.

As dependências são instaladas automaticamente com:

```` plaintext
npm install
````

Não é necessário instalar globalmente

Não é necessário instalar Vite, Chart.js ou Entity Framework Core manualmente. As versões utilizadas pelo projeto já estão definidas no package.json e no .csproj.