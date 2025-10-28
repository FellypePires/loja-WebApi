# 📘 Loja WebAPI – Sistema de Gestão de Pedidos e Produtos

## 🧾 Descrição Geral

O **Loja WebAPI** é uma aplicação backend desenvolvida em **ASP.NET Core 8** com **Entity Framework Core (SQLite)**, projetada para o **gerenciamento completo de produtos e pedidos**.

O sistema foi desenvolvido aplicando **boas práticas de arquitetura de software**, seguindo os princípios de **Clean Architecture** e **Domain-Driven Design (DDD)**.
Seu objetivo é demonstrar o domínio de conceitos avançados de desenvolvimento backend .NET, incluindo **persistência real**, **injeção de dependência**, **mapeamento de entidades**, **DTOs** e **documentação de API com Swagger**.

---

##  Arquitetura do Projeto

A solução foi estruturada em quatro camadas principais para promover desacoplamento, organização e facilidade de manutenção:

```
📦 Loja.API          → Camada de apresentação (Controllers, configuração e Swagger)
📦 Loja.Application  → Lógica de aplicação (Services, Interfaces, DTOs)
📦 Loja.Domain       → Entidades e regras de negócio
📦 Loja.Infra        → Persistência e infraestrutura (DbContext, Migrations, Repositório)
```

### 🖐️ Padrões Aplicados

* **Domain-Driven Design (DDD)**
* **Clean Architecture (camadas independentes)**
* **Repository Pattern (via EF Core)**
* **Dependency Injection (DI)**
* **DTOs (Data Transfer Objects)**
* **Async/Await (operações assíncronas)**

---

## ⚙️ Tecnologias Utilizadas

| Tecnologia                  | Descrição                                              |
| --------------------------- | ------------------------------------------------------ |
| **.NET 8.0**                | Framework principal                                    |
| **ASP.NET Core Web API**    | Criação da API REST                                    |
| **Entity Framework Core 8** | ORM para persistência de dados                         |
| **SQLite**                  | Banco de dados leve e local                            |
| **Swagger / Swashbuckle**   | Documentação interativa da API                         |
| **C# 12**                   | Linguagem de desenvolvimento                           |
| **LINQ / Async**            | Manipulação eficiente de dados e consultas assíncronas |

---

## 💿 Banco de Dados e Migrations

O projeto utiliza **SQLite** para armazenamento local.
As **migrations** são aplicadas automaticamente no startup da aplicação.

### Exemplo do `appsettings.json`

```json
"ConnectionStrings": {
  "LojaDb": "Data Source=loja.db"
}
```

Na primeira execução:

* O banco é criado automaticamente.
* Produtos iniciais são inseridos via **Seed de Dados**.
* Todas as tabelas e relacionamentos são configurados via **migrations automáticas**.

---

## 🚀 Funcionalidades Principais

### 💲 Módulo de Produtos

* Consulta de todos os produtos disponíveis
  → `GET /api/Produtos`
* Cadastro automático de produtos iniciais no banco.

### 📦 Módulo de Pedidos

* Criar pedidos com múltiplos produtos.
  → `POST /api/Pedidos`
* Adicionar produtos a um pedido existente.
  → `POST /api/Pedidos/{id}/Itens`
* Atualizar a quantidade de um item.
  → `PUT /api/Pedidos/{id}/Quantidade`
* Fechar pedido.
  → `POST /api/Pedidos/{id}/Fechar`
* Consultar um pedido detalhado.
  → `GET /api/Pedidos/{id}`

---

##  Exemplo de Uso (Swagger)

Após rodar o projeto, acesse o Swagger em:

🔗 **[http://localhost:5051](http://localhost:5051)**

---

### 🔹 Criar Pedido

**Endpoint:** `POST /api/Pedidos`

#### Corpo da Requisição:

```json
{
  "itens": [
    { "produtoId": 1, "quantidade": 2 },
    { "produtoId": 3, "quantidade": 1 }
  ]
}
```

#### Retorno Esperado:

```json
{ "id": 1 }
```

---

### 🔹 Consultar Pedido

**Endpoint:** `GET /api/Pedidos/1`

#### Exemplo de Resposta:

```json
{
  "id": 1,
  "status": "Aberto",
  "total": 529.70,
  "itens": [
    {
      "produtoId": 1,
      "produtoNome": "Camiseta Premium",
      "precoUnitario": 89.90,
      "quantidade": 2,
      "subtotal": 179.80
    },
    {
      "produtoId": 3,
      "produtoNome": "Tênis Esportivo",
      "precoUnitario": 349.90,
      "quantidade": 1,
      "subtotal": 349.90
    }
  ]
}
```

---

## 🧠 Regras de Negócio Implementadas

* **Pedidos iniciam com status “Aberto”.**
* **Pedidos fechados não podem ser alterados.**
* **Validação de produtos inexistentes.**
* **Cálculo automático de subtotal e total.**
* **Persistência real no banco SQLite.**

---

## 🧱 Estrutura Técnica

| Camada          | Responsabilidade                          |
| --------------- | ----------------------------------------- |
| **Domain**      | Entidades, enums e regras de negócio      |
| **Application** | Serviços e lógica de aplicação            |
| **Infra**       | Banco de dados e persistência             |
| **API**         | Controllers, rotas e documentação Swagger |

---

## 🧽 Como Executar o Projeto Localmente

### 1️⃣ Clonar o Repositório

```bash
git clone https://github.com/seuusuario/LojaWebAPI.git
cd LojaWebAPI
```

### 2️⃣ Restaurar Dependências

```bash
dotnet restore
```

### 3️⃣ Compilar o Projeto

```bash
dotnet build
```

### 4️⃣ Executar a Aplicação

```bash
dotnet run --project Loja.API
```

O Swagger será aberto automaticamente no navegador.

---

## 📋 Boas Práticas Aplicadas

* Separacão de responsabilidades (**SRP**)
* Uso extensivo de **async/await**
* **Injeção de dependência (DI)** nativa do ASP.NET Core
* Entidades encapsuladas e uso de **DTOs**
* Código limpo e comentado
* Automção de **migrations** e **seed**
* Aderência aos princípios **SOLID**

---

## 👨‍💻 Autor

**Fellype Gabriel Pires**
Desenvolvedor Backend .NET

📧 *[fellype.pires@insidesistemas.com.br](mailto:fellype.pires@insidesistemas.com.br)*
🔗 [GitHub – FellypePires](https://github.com/FellypePires)

---

## 💼 Considerações Finais

O projeto **Loja WebAPI** foi desenvolvido com foco em **profissionalismo, clareza de arquitetura e boas práticas de engenharia de software**.
Demonstra **proficiência em C#, .NET 8, DDD, Clean Architecture e EF Core**, sendo ideal para **avaliações técnicas e portfólios profissionais**.

Esta solução é funcional, escalável e reflete o padrão técnico exigido em ambientes corporativos.

---

## 🫋 Publicação no GitHub

### 1️⃣ Inicializar Repositório

```bash
git init
```

### 2️⃣ Adicionar Arquivos

```bash
git add .
```

### 3️⃣ Criar Commit Inicial

```bash
git commit -m "Versão inicial da Loja WebAPI"
```

### 4️⃣ Conectar ao Repositório Remoto

```bash
git remote add origin https://github.com/seuusuario/LojaWebAPI.git
```

### 5️⃣ Enviar para o GitHub

```bash
git push -u origin main
```

Após o push, o README será exibido automaticamente na página principal do repositório.

---

## 🗾 Licença

Distribuído sob a licença **MIT**, permitindo uso, modificação e distribuição com atribuição de crédito ao autor.

---

### ✅ Resultado Final

Um backend **completo, documentado e padronizado**, desenvolvido com **boas práticas profissionais** e ideal para **demonstração técnica e apresentação em processos seletivos**.
