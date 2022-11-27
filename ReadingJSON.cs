using Newtonsoft.Json;

[SerialisedField] string filepath;
 
void readJsonFile(){
  JsonTextReader reader = new JsonTextReader(new StringReader(File.ReadAllText(filePath)));
  while (reader.Read())
  {
      if (reader.Value != null) {
          if( reader.TokenType == JsonToken.PropertyName ){
              Debug.Log($"Key Name: {reader.Value}");     
          }
      }
  }
}

