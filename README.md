# Sistema de Controle de Chamados

Projeto acadêmico desenvolvido para a disciplina **Linguagem de Programação I**, com o objetivo de implementar um **sistema de abertura e controle de chamados** utilizando **ASP.NET Core MVC**.

## 📌 Funcionalidades
- Abrir um chamado (Id sugerido automaticamente pelo sistema).
- Listar chamados existentes.
- Alterar e excluir chamados.
- Cadastrar usuários (Id e Nome).
- Regras específicas:
  - Quando a situação for **Atendido**, os campos de atendimento tornam-se obrigatórios.
- Todas as **validações são feitas nos Controllers** (não usar `required` nos campos HTML).

## 🛠️ Tecnologias
- ASP.NET Core MVC
- C#
- Entity Framework Core
- SQL Server

## 🚀 Como executar
1. Clonar este repositório:
   ```bash
   git clone https://github.com/seu-usuario/controle-chamados.git
