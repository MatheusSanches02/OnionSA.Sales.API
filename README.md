Projeto: OnionSA.Sales.API -> Api controladora de vendas da empresa OnioSA

Projeto desenvolvido utilizando C# com .Net 7.0 e Entity Framework como orm, baseado no padrão repository onde temos a seguinte estrutura de pastas:
![image](https://github.com/MatheusSanches02/OnionSA.Sales.API/assets/79661325/917e66e4-d1e6-401b-bac8-bbef03adc384)

Visto que em Entities são implementadas da entidades do projeto; Em Repository temos a interface garantindo que o programa execute com os métodos corretos e implementando a interface, temos a classe repository, cujo qual fará acesso ao banco de dados através da ORM; Em Controllers, criamos um objeto de repository para acessarmos os métodos de acesso ao banco de dados; Em Persistence estão as Migrations e o arquivo de DbContext do projeto.

A string de conexão com o banco de dados, fica configurada em appsettings.json

Em Program.cs, deve-se adicionar a injeção de dependências necessária, no caso, como temos os métodos que acessam o banco de dados em Repository, devemos adicionar em Program.cs as seguintes linhas:

var conn = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SalesDbContext>(o => o.UseSqlServer(conn));

builder.Services.AddScoped<IArquivosRepository, ArquivosRepository>();
builder.Services.AddScoped<IPedidosRepository, PedidosRepository>();

Para configuração de CORS:

 #region [Cors]
    builder.Services.AddCors(c => c.AddPolicy("CorsPolicy", build =>
    {
        build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    }));
#endregion

#region [Cors]
    app.UseCors("CorsPolicy");
#endregion

Para rodar o projeto, estou utilizando alguns pacotes do NuGet, esses são:

Os funcionais do Entity Framework:

![image](https://github.com/MatheusSanches02/OnionSA.Sales.API/assets/79661325/977d990c-5f54-4eac-b354-03cee898c5be)
![image](https://github.com/MatheusSanches02/OnionSA.Sales.API/assets/79661325/37709de6-7929-410a-93d4-e1360d2126dc)

E no meu caso usando banco de dados SQL Server:

![image](https://github.com/MatheusSanches02/OnionSA.Sales.API/assets/79661325/037ba2ed-ed20-4710-a2a6-1efb462dc4b5)

Para leitura de planilhas Excel:

![image](https://github.com/MatheusSanches02/OnionSA.Sales.API/assets/79661325/143ead82-7ad5-41dc-bf37-201a9eefe42e)

Para tratar Json, no meu caso deserializar as informações obtidas no ViaCep:

![image](https://github.com/MatheusSanches02/OnionSA.Sales.API/assets/79661325/69e90033-a715-4d87-9da0-8d06aba691c1)


Script de criação de produtos no banco de dados:

INSERT INTO Produtos
(Nome, Valor)
VALUES 
  ('Celular', 1000),
  ('Notebook', 3000),
  ('Televisão', 5000);

