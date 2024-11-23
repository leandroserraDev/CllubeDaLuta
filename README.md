Clube da Luta

```markdown
# API de Lutas - .NET Core + PostgreSQL

Este repositório contém uma API RESTful desenvolvida com **.NET Core** e **PostgreSQL**. A aplicação permite que lutadores se conectem, marquem lutas e acompanhem seus resultados.

## Requisitos

- **.NET Core 6 ou superior**
- **PostgreSQL** (qualquer versão compatível com a biblioteca Npgsql)
- **Docker** (opcional, para rodar o PostgreSQL em container)
- **Visual Studio Code** ou qualquer IDE que suporte .NET Core

## Configuração do Ambiente

### 1. Clone o repositório

```bash
git clone https://github.com/seu-usuario/api-lutas.git
cd api-lutas
```

### 2. Instale as dependências

Instale os pacotes necessários para o projeto. No diretório do projeto, execute:

```bash
dotnet restore
```

3. Configuração do Banco de Dados

   Usando Docker (opcional)

Você pode rodar um container PostgreSQL com o seguinte comando (certifique-se de ter o Docker instalado):

```bash
docker run --name postgres-lutas -e POSTGRES_PASSWORD=mysecretpassword -p 5432:5432 -d postgres
```

  Conexão ao Banco de Dados

No arquivo `appsettings.json`, configure a string de conexão para o PostgreSQL:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Username=postgres;Password=mysecretpassword;Database=lutasdb;"
  }
}
```

Se você estiver utilizando Docker, a string de conexão pode ser:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Username=postgres;Password=mysecretpassword;Database=lutasdb;"
  }
}
```

### 4. Criar Banco de Dados

No terminal, navegue até o diretório do projeto e execute as migrações para criar o banco de dados:

```bash
dotnet ef database update
```

### 5. Executar a API

Para rodar a API em ambiente de desenvolvimento, execute:

```bash
dotnet run
```

A API estará disponível no endereço:

```
http://localhost:5000
```

### 6. Testar os Endpoints

A API fornece endpoints RESTful para interagir com os dados dos lutadores e das lutas. Aqui estão alguns exemplos de endpoints:

- `GET /api/lutadores` - Lista todos os lutadores.
- `GET /api/lutadores/{id}` - Obtém detalhes de um lutador específico.
- `POST /api/lutadores` - Cria um novo lutador.
- `PUT /api/lutadores/{id}` - Atualiza os dados de um lutador.
- `DELETE /api/lutadores/{id}` - Exclui um lutador.

### 7. Configuração de Logs

A API já está configurada para logs básicos. Você pode configurar o nível de log desejado no `appsettings.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
```

## Dependências

- **.NET Core 6+**
- **Npgsql** (biblioteca PostgreSQL para .NET)
- **EF Core** (Entity Framework Core para gerenciamento de banco de dados)

## Contribuindo

1. Faça um fork deste repositório.
2. Crie uma branch para suas mudanças (`git checkout -b minha-alteracao`).
3. Faça commit das suas alterações (`git commit -am 'Adicionando nova funcionalidade'`).
4. Faça push para sua branch (`git push origin minha-alteracao`).
5. Crie um novo Pull Request.

## Licença

Este projeto está licenciado sob a MIT License - veja o arquivo [LICENSE](LICENSE) para mais detalhes.

```

### Explicação:
- **Requisitos**: Instruções de instalação do **.NET Core**, **PostgreSQL**, e **Docker** (caso o banco seja executado em container).
- **Configuração do Banco de Dados**: Passos para configurar a conexão com o PostgreSQL, incluindo a opção de rodar o banco via Docker.
- **Execução da API**: Instruções para rodar a API localmente e testar os endpoints.
