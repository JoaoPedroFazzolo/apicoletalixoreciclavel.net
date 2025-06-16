# API Coleta Lixo Reciclável

## 📋 Sobre o Projeto

API REST para gerenciamento de coleta de lixo reciclável, desenvolvida em .NET 8 com Entity Framework Core e Oracle Database. O sistema permite o controle de resíduos eletrônicos, pontos de coleta, usuários, notificações e relatórios.

## 🚀 Tecnologias Utilizadas

- **.NET 8** - Framework principal
- **Entity Framework Core** - ORM
- **Oracle Database** - Banco de dados
- **AutoMapper** - Mapeamento de objetos
- **JWT Bearer** - Autenticação
- **Swagger** - Documentação da API
- **xUnit** - Testes unitários
- **BCrypt** - Hash de senhas

## 📦 Funcionalidades

- **Usuários**: Cadastro, autenticação e gerenciamento
- **Resíduos Eletrônicos**: Controle de equipamentos para descarte
- **Pontos de Coleta**: Gerenciamento de locais de coleta
- **Notificações**: Sistema de alertas para usuários
- **Relatórios**: Geração de relatórios do sistema
- **Alertas**: Monitoramento de situações críticas
- **Destinações**: Controle de destinos dos resíduos

## 🛠️ Instalação e Configuração

### Pré-requisitos

- .NET 8 SDK
- Oracle Database
- Visual Studio Code ou Visual Studio

### Configuração

1. **Clone o repositório**

   ```bash
   git clone <url-do-repositorio>
   cd apicoletalixoreciclavel.net
   ```

2. **Configure a string de conexão**

   Edite o arquivo `appsettings.json`:

   ```json
   {
     "ConnectionStrings": {
       "OracleConnection": "sua-string-de-conexao-oracle"
     }
   }
   ```

3. **Execute as migrações**

   ```bash
   dotnet ef database update
   ```

4. **Execute a aplicação**
   ```bash
   dotnet run
   ```

## 📚 Documentação da API

A documentação completa está disponível via Swagger em:

```
https://localhost:5001/swagger
```

### Principais Endpoints

- **GET** `/api/v1/Usuario` - Lista usuários
- **POST** `/api/v1/Usuario` - Cria usuário
- **GET** `/api/v1/ResiduoEletronico` - Lista resíduos
- **POST** `/api/v1/ResiduoEletronico` - Cadastra resíduo
- **GET** `/api/v1/Notificacao/usuario/{id}` - Notificações do usuário
- **GET** `/api/v1/Relatorio` - Lista relatórios

## 🔒 Autenticação

A API utiliza JWT Bearer Token. Para acessar endpoints protegidos:

1. Faça login via `/api/v1/Auth/login`
2. Use o token retornado no header: `Authorization: Bearer {token}`

## 🧪 Testes

Execute os testes unitários:

```bash
dotnet test
```

Os testes cobrem:

- Services (UsuarioService, ResiduoEletronicoService, PontoColetaService)
- Validações de entrada
- Casos de erro e sucesso

## 📁 Estrutura do Projeto

```
├── Controllers/          # Controladores da API
├── Models/              # Modelos de dados
├── ViewModel/           # ViewModels para entrada/saída
├── Services/            # Lógica de negócio
├── Data/               # Contexto e Repositórios
├── Tests/              # Testes unitários
└── Program.cs          # Configuração da aplicação
```

## 🔧 Configurações Importantes

- **Versionamento**: API v1.0 configurada
- **CORS**: Habilitado para desenvolvimento
- **Logging**: Configurado para ambiente de desenvolvimento
- **Validação**: Data Annotations nos ViewModels

## 🤝 Contribuição

1. Faça fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/nova-feature`)
3. Commit suas mudanças (`git commit -m 'Adiciona nova feature'`)
4. Push para a branch (`git push origin feature/nova-feature`)
5. Abra um Pull Request

## 📄 Licença

Este projeto está sob a licença MIT.

---

**Desenvolvido para gerenciamento sustentável de resíduos eletrônicos** 🌱
