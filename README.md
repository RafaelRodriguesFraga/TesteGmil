# TesteGmil
Este é um teste feito para a Empresa Gmil.

## **:hammer: Ferramentas Utilizadas**

- AutoMapper
- Mediator
- SqlServer
- EntityFrameworkCore
- Linq
- CQRS

## Executando

Antes de mais nada, temos que configurar a connectionString do banco de dados deixada propositalmente no git

```json
"ConnectionStrings": {
    "SqlServerConnection": "Data Source=localhost;Initial Catalog=testegmil;Persist Security Info=True;User ID=sa;Password=seuPassword;TrustServerCertificate=True;"
  },
```

Substitua o "seuPassword" pelo password do seu banco de dados.

Após isso, abra o Package Manager Console (View > Other Windows > Package Manager Console) e use o seuinte comando: 

`Update-Database` ou `Update-Database -P TesteGmil.Model`

Isso fará com que ele crie todas as tabelas do banco baseadas na Migration que está no projeto `TesteGmil.Model`. 
Vale lembrar que é necessário estar com o pacote `Microsoft.EntityFrameworkCore.Tools` para que esse comando funcione

## Usando a API

Execute o projeto e será mostrado varias rotas. Cadastre da seguinte ordem:

 1. Artists
 2. Genres
 3. Musics

O motivo disso é por causa dos relacionamentos entre eles. Quando for testar o delete, será necessario tanto um ArtistId quanto um MusicId,
então é necessário usar nesta ordem. Assim vale pro cadastro de músicas, sendo necessario um ArtistId e um GenderId.

## Conclusão

Esse foi um projeto para um teste feito para a empresa Gmil como parte do processo seletivo.
    




