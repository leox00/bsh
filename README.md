# BSH - Better Shell

**BSH (Better Shell)** is a lightweight, customizable (not yet) shell built in C#. Designed for Linux users, it offers essential features like command execution, history management, and more. It’s simple, fast, and an excellent starting point for anyone looking to experiment with creating their own shell.

## 🚀 Features

- **Command Execution**: Run Linux commands directly within BSH.
- **History Management**:
    - Navigate through previous commands with arrow keys.
    - Persistent history saved in `~/.bsh_history`.
- **Lazy Loading**: Efficient command history management for large history files.

## 🔨 Installation

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- Linux (recommended for full compatibility)

### Steps
1. Clone the repository:
   ```bash
   git clone https://github.com/leox00/bsh.git
   cd bsh
   ```

2. Build the project:
   ```bash
   dotnet build
   ```

3. Run BSH:
   ```bash
   dotnet run --project BSH
   ```

## 🔧 Configuration

### History File
BSH stores command history in `~/.bsh_history`:
- Format: `: <timestamp>;<command>`
- Example:
  ```
  : 1674234567;ls -la
  : 1674234570;cat file.txt
  ```

To clear history:
```bash
rm ~/.bsh_history
```

## 📚 Usage

- **Run Linux commands**:
  ```
  > ls -la
  ```
- **Navigate History**:
    - Press `↑` or `↓` to scroll through command history.
- **Exit BSH**:
  ```
  > exit
  ```

## 🛡️ License

This project is licensed under the [MIT License](LICENSE).

## 💡 Ideas for Future Features / ToDo
- **Custom commands**: Create a way to add custom commands.
- **App execution**: Running terminal apps like `nano`.
- **Command Aliases**: Add support for command aliases like `ll` for `ls -la`.
- **Auto-Completion**: Add auto-completion for commands and file paths.
- **Themes**: Add support for custom shell themes and prompt styles.
- **Command Suggestions**: Show suggestions for mistyped commands.

## 🤝 Contributing

We welcome contributions! If you'd like to contribute:
1. Fork this repository.
2. Create a feature branch:
   ```bash
   git checkout -b feature-name
   ```
3. Commit your changes:
   ```bash
   git commit -m "Add some feature"
   ```
4. Push to your branch:
   ```bash
   git push origin feature-name
   ```
5. Create a pull request.

## 📞 Support

If you encounter issues or have suggestions, feel free to [open an issue](https://github.com/leox00/bsh/issues).

## ⭐ Show Your Support

If you like this project, please give it a star ⭐ on GitHub. Your support means a lot!
