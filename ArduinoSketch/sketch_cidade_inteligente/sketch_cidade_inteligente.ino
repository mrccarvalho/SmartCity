#include <SPI.h>
#include <WiFiS3.h>

//INÍCIO - DEFINIÇÕES DE VARIÁVEIS E OBJETOS
///////colocar dados sensíveis em ficheiro separado/arduino_secrets.h
// dados Wi-Fi 
char ssid[] = "Galaxy-RC";          //  SSID (name) / 
char pass[] = "ttqo3746";   //  senha 
int status = WL_IDLE_STATUS;                     // Wifi radio's status
//  site URL para onde serão enviados os dados
char* host = "46.105.31.193";
const int postPorta = 8083;
int distance;
//int estacionamentoId;
// Geralmente, devemos usar "unsigned long" para variáveis que armazenam tempo
unsigned long previousMillis = 0;        // irá armazenar a última vez que foi lido
const long postDuracao = 10000; //intervalo entre cada Post/Gravação na base de dados
unsigned long ultimoPost = 0;
bool connected = false;
WiFiClient client;

// Definição dos pinos dos sensores e LEDs
const int trigPins[4] = {2, 4, 6, 8  };
const int echoPins[4] = {3, 5, 7, 9};
const int ledPins[4] = {10, 11, 12, 13};
 int sensor[4] = {1, 2, 3, 4  };
//FIM - DEFINIÇÕES DE VARIÁVEIS E OBJETOS
/*
 * Método WIFICONNECT para efetuar a ligação a uma determinada rede
 * wi-fi através de ssid e password
 */
void wificonnect(char ssid[], char pass[]) {
  Serial.begin(9600); //INICIALIZA A SERIAL
    while (!Serial) {
    ; // espera pela porta série para conectar. 
  }
  delay(500);
  // verificar de existe o módulo wifi:
  if (WiFi.status() == WL_NO_MODULE) {
    Serial.println("Communicação com o módulo wifi falhou!");
    // não continúa
    while (true);
  }
  // verificar se o firmware do módulo wireless está atualizado:
  String fv = WiFi.firmwareVersion();
  if (fv < WIFI_FIRMWARE_LATEST_VERSION) {
    Serial.println("Por favor faça upgrade do firmware");
  }
  // Tentativa de ligação à Rede Wifi:
  while (status != WL_CONNECTED) {
    Serial.print("A ligar à Rede Wifi, SSID: ");
    Serial.println(ssid);
    status = WiFi.begin(ssid, pass);
    // espera 10 segundos para tentar ligar de novo:
    delay(10000);
  }
Serial.println(WiFi.localIP());
  // definimos a duração do último post e fazemos o post imediatamente
  // desde que o main loop inicía
  ultimoPost = postDuracao;
  Serial.println("Setup completo");
  Serial.println("");
  Serial.println("Está conectado à rede");
}


// Função para medir a distância usando um sensor ultrassônico
int measureDistance(int trigPin, int echoPin) {
  // Gera um pulso no pino de trigger para iniciar a medição
digitalWrite(trigPin, LOW);
  delayMicroseconds(2);
  digitalWrite(trigPin, HIGH);
  delayMicroseconds(10);
  digitalWrite(trigPin, LOW);
  // Mede a duração do pulso no pino de eco
  long duration = pulseIn(echoPin, HIGH);
  // Converte a duração para centímetros
  int distance = duration * 0.034 / 2;
   
  return distance;

  
}
/* Método que realiza a tarefa de enviar as leituras 
 *para um serviço externo neste caso um servidor web  
 */
void post_dados(int estacionamento1, int estacionamento2,int estacionamento3, int estacionamento4) {
  Serial.println("Post/Envio de Dados - Início ");
  //int leitura = measureDistance();
  
  Serial.print(" - A conectar-se a ");
  Serial.println(host);
  Serial.print(" - na porta ");
  Serial.println(postPorta);  

  // Criar o URI para o request/pedido
  String url = String("/Home/PostData") + String("?estacionamento1=") + estacionamento1 + String("&estacionamento2=") + estacionamento2 + 
  String("&estacionamento3=") + estacionamento3 + String("&estacionamento4=") + estacionamento4;
  Serial.println(" - A solicitar o URL: ");
  Serial.print("     ");
  Serial.println(url);
  // envia o request/pedido para o servidor
  client.print(String("GET ") + url + " HTTP/1.1\r\n" +
               "Host: " + host + "\r\n" + 
               "Connection: close\r\n\r\n");
  delay(500);
  // Lê todas as linhas de resposta que vem do servidor web 
  // e escreve no serial monitor
  Serial.println(" - Resposta do SERVIDOR: ");
  while(client.available()){
    String line = client.readStringUntil('\r');
    Serial.print(line);
  }

  Serial.println("");
  Serial.println(" - A fechar a conexão");
  Serial.println("");
  Serial.println("Post/Envio Dados - FIM");
  Serial.println("");
}

void setup() {
     wificonnect(ssid, pass);
  Serial.begin(9600);

  for (int i = 0; i < 4; i++) {
    pinMode(trigPins[i], OUTPUT);
    pinMode(echoPins[i], INPUT);
    pinMode(ledPins[i], OUTPUT);
  }

}

void loop() {
for (int i = 0; i < 4; i++)
{
// Imprime a distância no monitor serial para cada sensor (opcional)
    Serial.print("Sensor ");
    Serial.print(i + 1);
    Serial.print(": ");
    Serial.print(measureDistance(trigPins[i], echoPins[i]));
    Serial.println(" cm");

  int dist = measureDistance(trigPins[i], echoPins[i]);
 // Verifica a distância e controla os LEDs correspondentes
    if (dist < 5) {
      digitalWrite(ledPins[i], LOW);  // Desliga o LED correspondente
    } else {
      digitalWrite(ledPins[i], HIGH); // Liga o LED correspondente
    }  
}

 for (int i = 0; i < 4; i++)
  {
   sensor[i+1] = measureDistance(trigPins[i], echoPins[i]);
 }

if (client.connect(host, postPorta)) {

   
    unsigned long diff = millis() - ultimoPost;
    if (diff > 500)
    {
      //estacionamentoId = 1;
      post_dados(sensor[1], sensor[2],sensor[3],sensor[4]);
      ultimoPost = millis();
    } else
    {
    Serial.println(" - Não conseguiu conectar-se ao host!");
    delay(1000);
    }

}
  delay(500);  // Aguarda meio segundo antes de realizar a próxima medição
}
