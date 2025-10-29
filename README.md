# 📘 **Loja WebAPI – Sistema de Gestão de Pedidos e Produtos**

![Status do Projeto](https://img.shields.io/badge/status-concluído-brightgreen)
![.NET 8](https://img.shields.io/badge/.NET-8.0-blueviolet)
![C# 12](https://img.shields.io/badge/C%23-12-blue)
![Licença](https://img.shields.io/badge/licen%C3%A7a-MIT-blue)


📖 Descrição Geral:

O Loja WebAPI é uma aplicação backend desenvolvida em ASP.NET Core 8 com Entity Framework Core (SQLite), projetada para o gerenciamento completo de produtos e pedidos.

O sistema foi desenvolvido aplicando boas práticas de arquitetura de software, seguindo os princípios de Clean Architecture e Domain-Driven Design (DDD).

Destaques técnicos:

Persistência real com Entity Framework Core

Injeção de dependência (DI)

Mapeamento de entidades e DTOs

Documentação interativa com Swagger


🏗️ Arquitetura do Projeto:

A solução foi estruturada em quatro camadas principais, garantindo organização, escalabilidade e facilidade de manutenção:

📦 Loja.API → Camada de apresentação (Controllers, configuração e Swagger)

📦 Loja.Application → Lógica de aplicação (Services, Interfaces, DTOs)

📦 Loja.Domain → Entidades e regras de negócio

📦 Loja.Infra → Persistência e infraestrutura (DbContext, Migrations, Repositório)


⚙️ Tecnologias Utilizadas:

Tecnologia	Descrição

.NET 8.0	Framework principal

ASP.NET Core Web API	Criação da API REST

Entity Framework Core 8	ORM para persistência de dados

SQLite	Banco de dados leve e local

Swagger / Swashbuckle	Documentação interativa da API

C# 12	Linguagem de desenvolvimento

LINQ / Async	Manipulação eficiente de dados assíncronos


💿 Banco de Dados – Primeira Execução:

Na primeira execução

🏗️ O banco é criado automaticamente
🛒 Produtos iniciais são inseridos via Seed de Dados
🔗 Todas as tabelas e relacionamentos são configurados via migrations automáticas


🚀 Funcionalidades Principais:

💲 Produtos

Método	Endpoint	Descrição

GET	/api/Produtos	Listar todos os produtos


📦 Pedidos:

Método	Endpoint	Descrição

POST	/api/Pedidos	Criar um novo pedido

POST	/api/Pedidos/{id}/Itens	Adicionar produtos a um pedido existente

PUT	/api/Pedidos/{id}/Quantidade	Atualizar a quantidade de um item

POST	/api/Pedidos/{id}/Fechar	Fechar um pedido

GET	/api/Pedidos/{id}	Consultar um pedido detalhado


📊 Relatórios (Diferencial)

Método	Endpoint	Descrição

GET	/api/pedidos/relatorio/saida-produtos	Relatório de saída consolidada de produtos


📌 Exemplo de Resposta:

[
  { "produtoNome": "Camiseta Premium", "quantidadeVendida": 5, "valorTotalVendido": 449.5 },
  
  { "produtoNome": "Tênis Esportivo", "quantidadeVendida": 2, "valorTotalVendido": 699.8 }
]


🔍 Nota técnica: Relatório otimizado em LINQ to Objects (in-memory), evitando erros de tradução SQL (RelationalGroupByShaperExpression).


🧑‍💻 Exemplo de Uso (Swagger):

Criar Pedido
POST /api/pedidos

{
  "itens": [
    { "produtoId": 1, "quantidade": 2 },
    
    { "produtoId": 3, "quantidade": 1 }
  ]
}


Consultar Pedido:

GET /api/pedidos/1

{
  "id": 1,
  
  "status": "Aberto",
  
  "total": 529.70,
  
  "itens": [
  
    { "produtoId": 1, "produtoNome": "Camiseta Premium", "precoUnitario": 89.90, "quantidade": 2, "subtotal": 179.80 },
    
    { "produtoId": 3, "produtoNome": "Tênis Esportivo", "precoUnitario": 349.90, "quantidade": 1, "subtotal": 349.90 }
  ]
}


🔗 Após rodar o projeto, acesse o Swagger em:
http://localhost:5051/swagger


🧠 Regras de Negócio Implementadas:

Regra	Descrição:
Status inicial	Pedidos sempre iniciam com “Aberto”

Bloqueio	Pedidos fechados não podem ser alterados

Validação	Produtos inexistentes não podem ser adicionados

Cálculo	Subtotal e total calculados automaticamente

Persistência	Banco de dados SQLite real


🧱 Estrutura Técnica:
Camada	Responsabilidade

Domain	Entidades, enums e regras de negócio

Application	Serviços, DTOs e lógica de aplicação

Infra	Persistência e banco de dados

API	Controllers, rotas e documentação Swagger


🧽 Como Executar o Projeto Localmente:
Etapa	Comando
1️⃣ Clonar repositório	git clone https://github.com/FellypePires/loja-WebApi.git && cd loja-WebApi

2️⃣ Restaurar dependências	dotnet restore

3️⃣ Compilar projeto	dotnet build

4️⃣ Executar aplicação	dotnet run --project Loja.API

🔗 O Swagger será aberto automaticamente no navegador.


📋 Boas Práticas Aplicadas:

✅ Separação de responsabilidades (SRP)

✅ Uso extensivo de async/await

✅ Injeção de dependência (DI)

✅ DTOs para encapsulamento e exposição limpa

✅ Código limpo, comentado e padronizado

✅ Automação de migrations e seed

✅ Aderência aos princípios SOLID


💡 Melhorias Futuras:
Técnicas

Autenticação e autorização com JWT/Identity

Validação com FluentValidation

Testes automatizados com xUnit

Operacionais / Visuais

Dashboard (Blazor/React) consumindo relatórios

Gráficos de produtos mais vendidos

Filtros por período/categoria nos relatórios


💡Arquitetura:

Pipeline de CI/CD com GitHub Actions

Dockerfile e deploy em Azure/Render


🧾 Troubleshooting (Erros Comuns e Soluções):

Erro	Causa	Solução

PedidoService não implementa interface	Métodos divergentes entre classe/interface	Corrigir assinaturas conforme commits finais

PedidoItem não contém definição Produto	Falta de navegação no EF	Usar ProdutoNome diretamente no DTO

Failed to fetch no Swagger	HTTPS local sem certificado	Acessar via http://localhost:<porta>

RelationalGroupByShaperExpression	LINQ tentou agrupar direto no SQL	Alterar para LINQ in-memory

Migrations não criam banco	Projeto alvo incorreto no console	Selecionar Loja.Infra e rodar Add-Migration


👨‍💻 Autor:

Fellype Gabriel Pires — Desenvolvedor Backend .NET

📧 fellype.pires@insidesistemas.com.br

🔗 GitHub – [FellypePires](https://github.com/FellypePires)


💼 Considerações Finais

O projeto Loja WebAPI foi desenvolvido com foco em profissionalismo, clareza de arquitetura e boas práticas.

Demonstra proficiência em C#, .NET 8, DDD, Clean Architecture e EF Core, sendo ideal para avaliações técnicas, processos seletivos e portfólios profissionais.

A solução é funcional, escalável e reflete o padrão técnico exigido em ambientes corporativos.


🗾 Licença

Distribuído sob a licença MIT, permitindo uso, modificação e distribuição com atribuição ao autor.


✅ Resultado Final

Um backend completo, documentado e padronizado, desenvolvido com boas práticas profissionais e ideal para demonstração técnica e apresentação em processos seletivos.
