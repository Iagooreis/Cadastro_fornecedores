# Cadastro de Fornecedores

Aplicação full stack para cadastro e gerenciamento de fornecedores, com backend em ASP.NET Core, frontend em Angular, autenticação JWT, validação de CNPJ, integração com BrasilAPI e ambiente Docker.

## Tecnologias

### Backend
- ASP.NET Core
- Entity Framework Core
- SQL Server
- JWT Bearer
- BCrypt
- BrasilAPI

### Frontend
- Angular
- TypeScript
- SCSS
- Angular Router
- HttpClient
- Guards e Interceptors

### Infraestrutura
- Docker
- Docker Compose
- Nginx
- SQL Server em container

## Funcionalidades

- Cadastro de fornecedor.
- Validação de CNPJ.
- Consulta de CNPJ via BrasilAPI.
- Senha armazenada com BCrypt.
- Login com JWT.
- Área restrita do fornecedor.
- Atualização dos dados do fornecedor logado.
- Login administrativo.
- Listagem administrativa de fornecedores.
- Proteção de rotas no frontend.
- Interceptor JWT no Angular.
- Aplicação dockerizada com backend, frontend e banco.

## Estrutura do Projeto

```text
cadastro_fornecedor/
├── backend/
│   ├── Controllers/
│   ├── Data/
│   ├── DTOs/
│   ├── Models/
│   ├── Services/
│   ├── Dockerfile
│   └── appsettings.json
├── frontend/
│   ├── src/app/core/
│   ├── src/app/features/
│   ├── src/app/shared/
│   ├── Dockerfile
│   ├── nginx.conf
│   └── proxy.conf.json
└── docker-compose.yml
Como Rodar com Docker
Pré-requisitos:

Docker Desktop instalado e em execução.
Na raiz do projeto, execute:

bash

docker compose up --build
Acesse o frontend em:

text


http://localhost:8080
Serviços criados:

Frontend: http://localhost:8080
Backend: http://localhost:5260
SQL Server: localhost,1433
Credenciais do SQL Server no Docker:

text


Servidor: localhost,1433
Usuario: sa
Senha: Fornecedor@12345
Banco: FornecedorDB
Para consultar os dados no banco Docker:

sql

USE FornecedorDB;
SELECT * FROM dbo.Fornecedores;
Para parar os containers:

bash

docker compose down
Para parar e apagar também o volume do banco:

bash

docker compose down -v
Observação: docker compose down mantém os dados do banco no volume Docker. O comando com -v apaga o volume e reinicia o banco do zero.

Como Rodar Localmente sem Docker
Backend
Pré-requisitos:

.NET SDK
SQL Server ou SQL Server Express
Banco FornecedorDB criado localmente
A connection string local está em backend/appsettings.json:

json

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=FornecedorDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
Entre na pasta do backend:

bash

cd backend
Restaure e compile:

bash

dotnet restore
dotnet build
Aplique as migrations:

bash

dotnet ef database update
Execute a API:

bash

dotnet run
Por padrão, a API local roda em:

text


http://localhost:5260
Frontend
Entre na pasta do frontend:

bash

cd frontend
Instale as dependências:

bash

npm install
Execute:

bash

npm start
ou:

bash

ng serve
Acesse:

text


http://localhost:4200
O frontend local usa proxy.conf.json para encaminhar chamadas /api para o backend em http://localhost:5260.

Credenciais de Teste
Admin:

text


Usuario: admin
Senha: admin123
Fornecedor:

Deve ser cadastrado pela tela de cadastro.
Depois do cadastro, faça login usando CNPJ e senha cadastrados.
Endpoints Principais
Fornecedor
http

POST /api/fornecedor
GET /api/fornecedor/me
PUT /api/fornecedor/me
Autenticacao
http

POST /api/auth/login
CNPJ
http

GET /api/cnpj/{cnpj}
Admin
http

GET /api/admin/fornecedores
Configuracoes no Docker
No docker-compose.yml, o backend recebe configurações por variáveis de ambiente:

yaml

ConnectionStrings__DefaultConnection
Jwt__Key
Jwt__Issuer
Jwt__Audience
Jwt__ExpireMinutes
Admin__User
Admin__Password
Isso permite alterar configurações sem modificar o código.

Observacoes de Seguranca
Este projeto mantém algumas credenciais de desenvolvimento para facilitar a execução local e a avaliação:

admin admin/admin123;
chave JWT de desenvolvimento no appsettings.json;
senha do SQL Server no docker-compose.yml.
Em um ambiente real de produção, essas informações deveriam ser armazenadas em:

variáveis de ambiente;
User Secrets;
cofres de segredo;
serviços gerenciados da plataforma de deploy.
A senha dos fornecedores não é armazenada em texto puro. Ela é salva usando BCrypt.

Decisoes Tecnicas
O frontend chama rotas relativas /api.
Em desenvolvimento local, o Angular usa proxy.conf.json.
Em Docker, o Nginx do frontend faz proxy de /api para o backend.
O backend aplica migrations automaticamente ao iniciar, facilitando a execução via Docker.
A integração com a BrasilAPI possui tratamento simples de erros externos, incluindo limite de requisições e indisponibilidade.
Observacao Sobre Bancos Local e Docker
O banco local e o banco Docker são diferentes.

Local sem Docker:

text


localhost\SQLEXPRESS
Docker:

text


localhost,1433
Usuario: sa
Senha: Fornecedor@12345
Cadastros feitos no Docker aparecem no banco Docker, não no banco local SQLEXPRESS.

Melhorias Futuras
Criar cadastro administrativo no banco em vez de admin fixo.
Usar refresh token.
Criar recuperação de senha.
Melhorar validações visuais no frontend.
Adicionar testes automatizados.
Publicar em ambiente cloud.
Usar serviço gerenciado de banco em produção.


