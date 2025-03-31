using transcribeApp;
var transcribeApi = new OpenAIClient("Open-AI-API-Key");//Your api key goes here.
Console.WriteLine("Lets transcribe using open AI");

string response = await transcribeApi.TranscribeAudio();
Console.WriteLine("Audio API Says: ");//response from open AI
Console.WriteLine(response);
Console.ReadLine();
