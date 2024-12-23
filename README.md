# MeBot: Personal AI Chatbot

**MeBot** is a personalized chatbot designed to answer questions about your professional experiences using context provided in a structured JSON file. The bot uses the Hugging Face Inference API to generate responses and provides an engaging console interface.

---

## Features

- **Custom Context**: Uses a JSON file (`context.json`) to provide information about your experiences.
- **Hugging Face Integration**: Leverages Hugging Face’s inference models for natural language processing.
- **Interactive Console**: Color-coded output for a better user experience.
- **Customizable and Expandable**: Easily adapt the bot for other contexts or functionalities.

---

## Prerequisites

- **.NET SDK**: Version 6.0 or higher.
- **NuGet Packages**:
  - [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json): For JSON serialization and deserialization.
- **Hugging Face API Key**: Obtain from [Hugging Face](https://huggingface.co/) and add it to the `ApiKey` constant in the code.
- **Context File**: Prepare a JSON file named `context.json` with your professional details.

---

## Installation

1. **Clone the Repository**:
    ```bash
    git clone https://github.com/dangodinho98/mebot.git
    cd mebot
    ```

2. **Install Dependencies**:
    Ensure the required NuGet packages are installed:
    ```bash
    dotnet add package Newtonsoft.Json
    ```

3. **Prepare Context**:
    Create a `context.json` file with the following structure:
    ```json
    {
        "summary": "Your professional summary",
        "skills": ["Skill1", "Skill2"],
        "experiences": [
            {
                "role": "Your Role",
                "company": "Your Company",
                "description": "What you did"
            }
        ]
    }
    ```

4. **Set Up Hugging Face API Key**:
    Replace the `ApiKey` constant in the `Program.cs` file with your Hugging Face API key.

5. **Run the Application**:
    ```bash
    dotnet run
    ```

---

## Usage

1. **Start the Bot**:
    When you run the application, the bot welcomes you and prompts for questions.

2. **Ask Questions**:
    Type questions related to the provided context, and the bot will respond using the Hugging Face model.

3. **Exit the Conversation**:
    Type `/bye` to end the session.

---

## Example Conversation

![image](https://github.com/user-attachments/assets/c80211ab-d487-405e-b23b-e66c925b6733)

---

## Code Overview

### `Program.cs`
- **Main Entry Point**: Handles user input and manages the conversation flow.
- **API Integration**: Sends questions and context to the Hugging Face API and processes the responses.
- **Utilities**:
  - `GetAnswerFromModelAsync`: Communicates with the Hugging Face API.
  - `BotReply`: Outputs responses in a distinct color.

### JSON Context File (`context.json`)
- Stores user-provided context information for the bot.
- Easily customizable to include various details about professional experiences, skills, and more.

---

## Future Enhancements

- **Multi-Language Support**: Expand to support languages other than English.
- **Additional Context Formats**: Allow importing context from other file formats (e.g., XML, YAML).
- **GUI Interface**: Add a graphical user interface for a more interactive experience.

---

## Limitations

- **API Dependency**: Requires internet access and a valid Hugging Face API key.
- **Context Scope**: Limited by the information provided in the `context.json` file.

---

## License
This project is licensed under the MIT License.

---

Enjoy using MeBot!

