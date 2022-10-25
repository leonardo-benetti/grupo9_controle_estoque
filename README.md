# Grupo9 - Controle de Estoque

## Proposta
Esse projeto tem por objetivo a criação de uma aplicação para Windows capaz de gerenciar o controle de estoque, utilizando C#, e os frameworks WPF e XAML.


## Como executar
1. Primeiramente é necessário uma versão do [Visual Studio](https://visualstudio.microsoft.com/pt-br/downloads/) instalada em seu computador (Windows).
2. Você pode obter o código fonte ao fazer o download do zip deste repositório ou utilizando a ferramenta [git](https://git-scm.com/book/en/v2/Getting-Started-Installing-Git) para realizar um `clone` deste repositório.
3. Depois disso, abra a solução no Visual Studio.
4. Para buildar e executar fora do modo de debug, basta utilizar `ctrl+f5` (ou clicar em "run" no topo do visual studio). 



## O repositório
O repositório está organizado na seguinte estrutura:
- `diretório base`: O diretório base contem o [arquivo de solução (.sln)](grupo9_controle_estoque.sln), em que são especificadas as versões mínimas do Visual Studio para executar a aplicação, além de fazer o link dos projetos envolvidos e configurações de build. Também possui arquivos importantes para o git e github, como o gitignore, arquivo de licença e README.
- `./controle_estoque/`: diretório que contém o projeto principal da aplicação, que está estruturado em uma arquitetura Model View Controler (MVC):
  ```
  .
  ├── Controller/                         # controladores para cara uma das classes criadas em Model
  │   └── *.cs
  ├── Model/                              # classes utilizadas como modelos na aplicação
  │   ├── ApplicationDbContext.cs           # utilizada para criar o contexto do banco de dados no EntityFramework
  │   └── *.cs
  ├── PersistentData/                     # diretorio reservado para arquivos que persitem entre execuções distintas da aplicação
  │   ├── DB                                # diretorio onde fica o banco de dados (.db) local
  │   ├── Icons                             # diretório para armazenarmos os ícones utilizados na aplicação
  │   └── ProfilePictures                   # diretório para armazenar as fotos de perfil fornecidas pelo usuário
  ├── View/                               # arquivos que criam a interface que o usuário visualiza e as funções que são executadas com suas ações
  │   ├── *.xaml
  │   └── *.xaml.cs
  ├── App.xaml                            # configurações gerais da aplicação
  ├── App.xaml.cs
  ├── AssemblyInfo.cs                     # informações sobre o assembly
  └── grupo9_controle_estoque.csproj      # arquivo do projeto
  
  ```
- `./ControleEstoque.Themes/`: projeto utilizado para improtar os temas fornecidos pelo framework [MahApps.Metro](https://github.com/MahApps/MahApps.Metro)


## Licença

Copyright (c) Leonardo Benetti / Renan Ikeda. Todos direitos reservados.

Licensed under the [MIT](LICENSE.txt) license.

