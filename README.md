# 🎬 API REST para Gerenciamento de Filmes

Desenvolvi uma API para gerenciar **Filmes**, **Filmmakers** e **Gêneros**, implementando operações de **CRUD** sobre um banco de dados relacional.

## 🚀 Tecnologias e recursos utilizados

- .NET 8 / C#
- Entity Framework Core com Migrations
- Swagger / OpenAPI
- Injeção de dependência com `DAL<T>`
- Endpoints minimalistas (**Minimal APIs**) com `MapGet`, `MapPost`, `MapPut`, `MapDelete`
- Hospedagem em **Azure**
- Relacionamentos entre entidades: `Filmmaker → Filmes → Gêneros`

## ✅ Funcionalidades da API

- Cadastrar, editar e remover **Filmmakers**, **Filmes** e **Gêneros**
- Buscar registros por **ID** ou **nome**
- Gerenciar relações entre os dados  
  (por exemplo, um filme pertence a um gênero e é associado a um filmmaker)
