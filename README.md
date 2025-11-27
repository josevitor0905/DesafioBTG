# DesafioBTG
Criar sistema de Cartão de Vacinação

## Descrição do Projeto

Este projeto consiste na criação de um sistema para o **Gerenciamento de Dados de Vacinação**. A aplicação permite cadastrar pessoas e vacinas, registrar a aplicação de doses e consultar a carteira nacional de vacinação individual.

A solução é dividida em duas partes: um **Backend em C# (.NET Core Web API)** e um **Frontend em Angular**.

---

## Tecnologias Utilizadas

| Componente | Tecnologia | Versão | Notas |
| :--- | :--- | :--- | :--- |
| **Backend** | C# .NET Core Web API | .NET 10.0 | Versão atualizada para compatibilidade. |
| **Banco de Dados** | Entity Framework Core (EF Core) | 8.0.11 | Banco de dados em memória (`InMemory`). |
| **Frontend** | Angular | CLI Instalado | Estilização com Angular Material e CSS. |
| **Controle de Versão** | Git | - | Utilização do padrão GitFlow. |

---

## Como Instalar e Rodar o Projeto

### Pré-requisitos

Para rodar o projeto localmente, você precisa ter o seguinte instalado:

* **SDK do .NET Core** (Versão 10.0 ou superior).
* **Node.js** e **npm**.
* **Angular CLI** (`npm install -g @angular/cli`).

### 1. Backend (API)

1.  Navegue até o diretório da pasta do Backend no seu terminal `cd backend`.
2.  Execute o comando para rodar a API:
    ```bash
    dotnet run
    ```
    *A API estará acessível em `http://localhost:5011`.*

### 2. Frontend (Angular)

1.  Navegue até o diretório da pasta do Frontend no seu terminal `cd frontend`.
2.  Instale as dependências:
    ```bash
    npm install
    ```
3.  Execute a aplicação Angular:
    ```bash
    ng serve --open
    ```
    *A aplicação será aberta em `http://localhost:4200`.*

---

## Arquitetura e Funcionalidades

### Backend (.NET Web API)

A arquitetura do Backend utiliza camadas (Repository, Service e Controller) e DTOs para garantir a segurança e organização do código.

* **Services:** Implementam a lógica de negócios, como validações das doses e orquestração dos repositórios.
* **DTOs:** Utilizados para evitar *loops* de serialização e expor apenas dados necessários na API.

| Ação | Método | URL | Status de Teste |
| :--- | :--- | :--- | :--- |
| Criar Pessoa | `POST` | `/api/Pessoa` | OK |
| Consultar Pessoas | `GET` | `/api/Pessoa` | OK |
| Deletar Pessoa | `DELETE` | `/api/Pessoa/{id}` | OK |
| Consultar Carteira | `GET` | `/api/Pessoa/{id}/cartao` | OK |
| Criar Vacina | `POST` | `/api/Vacina` | OK |
| Consultar Vacinas | `GET` | `/api/Vacina` | OK |
| Deletar Vacina | `DELETE` | `/api/Vacina/{id}` | OK |
| Criar Vacinação | `POST` | `/api/Vacinacao` | OK |
| Deletar Vacinação | `DELETE` | `/api/Vacinacao/{id}` | OK |


---

## Desafios e Soluções (V1.0)

O desenvolvimento desta versão 1.0 exigiu a solução de problemas específicos de sincronização e fluxo:

* **Problema de Compatibilidade:** Inicialmente, houve erros de compatibilidade do EF Core, resolvidos com a **atualização do SDK do .NET para a versão 10.0**.
* **Comunicação Frontend/Backend:** Falha na comunicação resolvida ao utilizar **HTTP em vez de HTTPS**.
* **Detecção de Mudanças (Bug da Tela):** O problema de a tela não atualizar após ações (como selecionar no `<select>`) foi corrigido permanentemente utilizando o **`ChangeDetectorRef`** do Angular.
* **Exclusão de Pessoa Vazia:** Corrigido o *bug* onde não era possível deletar uma pessoa sem vacinações, ajustando o fluxo assíncrono do RxJS para garantir a continuidade da cadeia de exclusão.

---

## Documentação Completa

Para detalhes de arquitetura, requisitos e especificações completas, consulte a documentação oficial do projeto:

[Link para o Google Docs (Documentação Completa)](<https://docs.google.com/document/d/1Ln6SyrPSs9o-HsDPOq-NvzrmGwwCNhweyHlWLjWBPQA/edit?usp=sharing>)
