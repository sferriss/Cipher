// See https://aka.ms/new-console-template for more information

using Cipher;
using Cipher.Handlers.Ciphers;
using Cipher.Handlers.Ciphers.Interfaces;
using Cipher.Handlers.Files;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddTransient<IFileHandler, FileHandler>();
services.AddTransient<ICipherHandler, CipherHandler>();
services.AddTransient<IKeyCipherHandler, KeyCipherHandler>();

services.AddTransient<RunProject>();

services.BuildServiceProvider().GetService<RunProject>()!.Run();