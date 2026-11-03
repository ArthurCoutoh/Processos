# processos

Projeto Blazor Web App (ASP.NET Core) com MySQL para **listar, cadastrar e editar processos**, seguindo os materiais de PDS **Aula 08/09 — Listagem de registros** e **Implementação — Edição de Processo**.

## Funcionalidades

- `/processos`: lista registros e mostra o botão de lápis para editar.
- `/processos/cadastro`: usa formulário com validação e faz `INSERT`.
- `/processos/editar/{id}`: consulta o processo, preenche o mesmo formulário e faz `UPDATE`.
- ID inexistente: mostra **Processo não encontrado**.
- Cancelar: volta à listagem sem salvar.
- Situação: Aberto, Em análise, Pendente, Concluído ou Arquivado.
- Botão de excluir apenas ilustrativo e **desabilitado**, pois exclusão não foi solicitada.

## Pré-requisitos

- SDK .NET 9 e MySQL ativo.
- IDE compatível com Blazor / C#.

## Executar

1. Execute `Configs/script_banco_processos.sql` no MySQL Workbench.
2. Altere `ConnectionStrings:MySqlConnection` em `appsettings.json` com os dados **locais** do seu banco. Nunca publique senhas reais nesse arquivo.
3. Na pasta do projeto, rode:

```bash
dotnet restore
dotnet build
dotnet run
```

4. Abra a URL mostrada no terminal e entre em `/processos`.

## Arquivos principais

| Arquivo | Finalidade |
| --- | --- |
| `Model/Processo.cs` | Entidade e validação de campos |
| `Configs/Conexao.cs` | Abertura de conexão MySQL |
| `Configs/DAOHelper.cs` | Leitura segura de campos `NULL` |
| `DAO/ProcessoDAO.cs` | `Listar`, `ObterPorId`, `Inserir` e `Atualizar` |
| `Components/Pages/Processo/Index.razor` | Listagem e link de edição |
| `Components/Pages/Processo/Cadastro.razor` | Mesmo formulário para cadastrar/editar |
| `Program.cs` | Registro de serviços com `AddScoped` |

## Diferença de tipo de data nos dois PDFs

A aula 08/09 usa `DateTime?`, enquanto o PDF de edição exemplifica `DateOnly`. Este projeto **mantém `DateTime?` da aula 08/09** em todas as camadas para evitar incompatibilidades. `InputDate` suporta `DateTime?`; o DAO manda `DBNull.Value` quando a data está vazia e utiliza `DAOHelper.GetDateTime` ao consultar.

## Verificações manuais

1. Cadastro: preencher campos, salvar e verificar nova linha na listagem.
2. Edição: clicar no lápis, conferir campos preenchidos, alterar e atualizar.
3. Obrigatórios: tentar salvar com número, interessado ou assunto vazio.
4. ID inexistente: acessar `/processos/editar/999999`.
5. Cancelar: alterar algum campo e voltar sem salvar.

**Observação:** não foi possível executar `dotnet build` neste ambiente, porque o SDK .NET não está instalado. É necessário rodar os testes na sua máquina.
