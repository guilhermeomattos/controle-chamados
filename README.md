# 📌 Sistema de Controle de Chamados  

![GitHub repo size](https://img.shields.io/github/repo-size/seu-usuario/controle-chamados?style=for-the-badge)  
![GitHub contributors](https://img.shields.io/github/contributors/seu-usuario/controle-chamados?style=for-the-badge)  
![GitHub last commit](https://img.shields.io/github/last-commit/seu-usuario/controle-chamados?style=for-the-badge)  

---

## 🎓 Sobre o Projeto  
Este projeto foi desenvolvido como parte da disciplina **Linguagem de Programação I**, no **5º semestre do curso de Engenharia da Computação** da **Faculdade Engenheiro Salvador Arena**.  

O objetivo é implementar um **sistema de abertura e controle de chamados** em **ASP.NET Core MVC**, aplicando conceitos de **programação orientada a objetos, arquitetura MVC e integração com banco de dados**.  

---

## 🚀 Funcionalidades  
✔️ Abrir um chamado (Id gerado automaticamente pelo sistema)  
✔️ Listar todos os chamados registrados  
✔️ Alterar e excluir chamados existentes  
✔️ Cadastrar usuários (Id e Nome)  
✔️ Regras de negócio:  
   - Se a situação do chamado for **Atendido**, os campos de atendimento tornam-se obrigatórios  
✔️ Todas as **validações são feitas no Controller** (não utilizar `required` no HTML)  

---

## 🛠️ Tecnologias Utilizadas  
- **Linguagem:** C#  
- **Framework:** ASP.NET Core MVC  
- **Banco de Dados:** SQL Server (com Entity Framework Core)  
- **Ferramentas de Desenvolvimento:**  
  - Visual Studio / VS Code  
  - GitHub + GitHub Desktop  

---

## 📂 Estrutura do Projeto (prevista)  
controle-chamados/
│
├── src/
│ └── ControleChamados/ # Projeto ASP.NET Core MVC
│
├── .gitignore # Arquivos e pastas ignorados pelo Git
├── README.md # Documentação do projeto
└── ControleChamados.sln # Solução do Visual Studio


---

## ⚙️ Como Executar o Projeto

### Pré-requisitos
Antes de começar, você vai precisar ter instalado em sua máquina as seguintes ferramentas:
- [.NET SDK](https://dotnet.microsoft.com/download) (versão utilizada no projeto)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) (ou SQL Server Express)
- [Git](https://git-scm.com/)

### Rodando a Aplicação
```bash
# 1. Clone este repositório
git clone [https://github.com/seu-usuario/controle-chamados.git](https://github.com/seu-usuario/controle-chamados.git)

# 2. Acesse a pasta do projeto
cd controle-chamados

# 3. Configure a Connection String
# Abra o arquivo `appsettings.json` e altere a string de conexão do banco de dados
# para apontar para sua instância local do SQL Server.

# 4. Execute as migrações do Entity Framework para criar o banco de dados
dotnet ef database update

# 5. Execute a aplicação
dotnet run

# 6. Acesse http://localhost:5000 (ou a porta indicada no terminal) no seu navegador.


📸 Capturas de Tela

(Substitua estas imagens pelos prints do seu sistema quando estiver pronto)

🔹 Tela Inicial

🔹 Listagem de Chamados

🔹 Cadastro de Usuário

📚 Aprendizados Esperados

🔹 Prática de programação orientada a objetos em C#
🔹 Aplicação de padrão MVC em ASP.NET
🔹 Criação de CRUD completo (Create, Read, Update, Delete)
🔹 Uso do Entity Framework Core para persistência de dados
🔹 Aplicação de boas práticas com Git e GitHub

👨‍💻 Autores

<img src="https://avatars.githubusercontent.com/u/guilhermeomattos?v=4" width="100px;"/><br /><sub><b>Guilherme Mattos</b></sub>	<img src="https://avatars.githubusercontent.com/u/paulohcarvalho07?v=4" width="100px;"/><br /><sub><b>Paulo Henrique</b></sub>

Estudantes de Engenharia da Computação – 5º Semestre
Faculdade Engenheiro Salvador Arena


   

