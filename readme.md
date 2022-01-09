# Infected EP

Esse é um projeto no qual utilizamos uma aplicação mobile, uma api no back end e uma base de dados Portgres para entendeer se um dado usuario interagiu com um usuario potencialmente doente.

##Grupo
Paulo Daniel Carneiro - 10424993
Vinicius Henrique Saito de Almeida - 10816815

# Explicações de teste:
## Mobile
### Rodar o App
1. Intale O JDK 11
2. Faça o download e abra o Android Studios.
3. Adicione a variavel ANDROID_SDK_ROOT nas suas variaveis de ambiente.(No windows seria C:\Users\{{USER}}\AppData\Local\Android\Sdk)
4. Adicione o caminho para C:\Users\{{USER}}\AppData\Local\Android\Sdk\platform-tool no PATH
5. Clique em File->Settings, , Appearance & behavior -> System Settings -> Android SDK. 
Na aba SDK Tools verifique se Android Emulator está instalado.
6. Em seguida, abra o AVD Manager (Tools -> AVD Manager), e clique em Create Virtual Device.
7. Selecione a definição de dispositivo, a imagem do sistema e verifique as configurações e clique em Finish
(as configurações utilizadas na apresentação foram, 
definição de dispositivo Pixel 2 (5.0 1080x1920 xxhdpi), imagem de sistema R (Android 11.0 x86))
8. Clique em Run para começar a emular.
9. Com o terminal aberto na root do projeto, execute o comando -
npm install
npx react-native run-android
O primeiro comando instala as dependências e o segundo instala o aplicativo no emulador.
10. O aplicativo será aberto no emulador.

### Selecionar localização
Para indicar uma localização, clique em `...` 
Após clique em Location e escolha a localização no mapa 
clique no canto direito inferior Set Location
(é possivel salvar uma localização clicando em Save Point, mostrado quando voce clica em alguma parte do mapa)

## Database
Para acessar a base de dados, pode-se utilizar as seguintes credenciais:
host: database-1.cd4wnujqkcc1.us-east-2.rds.amazonaws.com
Port: 5432
User: root
password: rootroot

A partir disso é possivel verificar normalmente as tabelas e realizar queries.

## Backend
Por ser uma aplicação que o smartphone/emulador tem que se conectar por web, não é possivel rodar local e conectar o smartphone, entretanto é possivel rodar a aplicação e utilizar a interface swaggerUI para realizar modificações no estado da base. Logo, temos duas formas de interação: A API deployada na AWS e o swagger local.

### Swagger local

1. Intale o .Net 5.
2. Instale uma IDE caso queira analisar o código(Visual studio community é o suficiente, mas pode-se utilizar o vscode e o rider, caso esteja no linux/mac).
3. Para rodar a aplicação pode-se ir a pasta raiz da aplicação e rodar: 
   1. Intalar o docker. Executar o comando `docker build --no-cache -t infcted-backend -f Dockerfile .` seguido de `docker run -d -p 2000:80 counter-image:latest`. A aplicação está rodando no endereço localhost:2000
   2. OU `dotnet run --project .\InfectedBackEnd.Api\InfectedBackEnd.Api.csproj`. A aplicação está rodando no endereço locahost:5000.
4. A partir do passo anterior, pode ser adicionado /swagger ao final da url para acessar a interface swagger da aplicação, onde se tem acesso a todos os endpoints.
5. O primeiro endpoint que deve ser executado quando a base estiver vazia deve ser o post /users, para criar um usuario. A partir disso pode utilizar o token desse usuario para as outras requests. 
6. Para fazer algo com doençs, é necessario cadastrar a doença "Covid"por meio do post /diseases. Realizar apenas se a base estiver vazia.

### Swagger AWS

1. Já possuimos a aplicação deployada na AWS, no endereço http://18.117.176.243:2000/.
2. Da mesma forma que o local, pode-se adicionar `/swagger` no final da url para acessar a interface swagger, com acesso aos endpoints.
