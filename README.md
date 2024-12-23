# MeBot: A Chatbot That Knows You

MeBot is a personalized chatbot that answers questions about your experiences based on your resume. It uses text extracted from a PDF file of your resume as its context and communicates with a hypothetical AI model named Phi-3 for generating responses.

## Features
- Extracts text from your resume (PDF format) using the PdfPig library.
- Integrates with a hypothetical AI model (Phi-3) for natural language understanding and response generation.
- Enables interactive conversations about your professional experiences.
- Allows users to terminate the conversation gracefully by typing `/bye`.

---

## Prerequisites

- **.NET SDK**: Version 6.0 or higher.
- **NuGet Packages**:
  - [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json): For JSON serialization and deserialization.
  - [UglyToad.PdfPig](https://www.nuget.org/packages/UglyToad.PdfPig): For extracting text from PDFs.
- **Phi-3 API Key**: Replace `your-phi3-api-key` in the code with your actual API key.

---

## Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/dangodinho98/mebot.git
    cd mebot
    ```

2. Install the required NuGet packages:
    ```bash
    dotnet add package Newtonsoft.Json
    dotnet add package UglyToad.PdfPig
    ```

3. Build the Docker image:
    ```dockerfile
    # See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

    # This stage is used when running from VS in fast mode (Default for Debug configuration)
    FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
    USER $APP_UID
    WORKDIR /app

    # This stage is used to build the service project
    FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
    ARG BUILD_CONFIGURATION=Release
    WORKDIR /src
    COPY ["MeBot.csproj", "."]
    RUN dotnet restore "./MeBot.csproj"
    COPY . .
    WORKDIR "/src/."
    RUN dotnet build "./MeBot.csproj" -c $BUILD_CONFIGURATION -o /app/build

    # This stage is used to publish the service project to be copied to the final stage
    FROM build AS publish
    ARG BUILD_CONFIGURATION=Release
    RUN dotnet publish "./MeBot.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

    # This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
    FROM base AS final
    WORKDIR /app
    COPY --from=publish /app/publish .
    ENTRYPOINT ["dotnet", "MeBot.dll"]
    ```

4. Run the Docker container:
    ```bash
    docker build -t mebot .
    docker run -e Phi3ApiKey=your-phi3-api-key -e PdfFilePath=path_to_your_resume.pdf mebot
    ```

---

## Usage

1. Build and run the application:
    ```bash
    dotnet run
    ```

2. Follow the prompts in the console:
    - Type a question about your experiences.
    - Type `/bye` to exit the conversation.

3. Example conversation:
    ```plaintext
    Welcome to the 'Me' Bot! Ask me anything about my experiences, and I'll answer as if I were you.
    Type '/bye' to end the conversation.

    Please ask me a question!
    > What are your key technical skills?
    Daniel's answer: My technical skills include .NET development, C#, and building scalable applications.

    Please ask me a question!
    > /bye
    Goodbye!
    ```

---

## Code Overview

### `Program.cs`
- Entry point of the application.
- Handles user input and integrates with the Phi-3 API for response generation.

### `Helpers/PdfTextExtractor.cs`
- Extracts text from PDF files using the **PdfPig** library.
- Returns extracted text as a string.

### Phi-3 API Integration
- Sends user queries and the extracted resume text to the Phi-3 API.
- Receives and displays responses generated by the AI model.

---

## Limitations

1. **PDF Text Extraction**:
   - Works only with PDFs containing selectable text. For scanned/image-based PDFs, consider integrating OCR (e.g., Tesseract).

2. **Phi-3 Model**:
   - This implementation assumes a hypothetical AI API. Replace with a real AI model or service as needed.

---

