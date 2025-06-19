# Thoughts on a raft

A simple, elegant C# MVC blog application designed to capture the intimate feeling of writing in a personal journal.

## Features

### Core Functionality
- **Markdown Blog Posts**: Write posts in Markdown with YAML front matter
- **Date-based Organization**: Posts stored as `YYYYMMDD.md` files
- **Pagination**: Clean pagination system (10 posts per page)
- **Tag System**: Categorize posts with clickable tags
- **Smart Navigation**: Back buttons remember which page you came from

### Design & Themes
- **Multiple Themes**: 4 beautiful themes to choose from
  - **Light**: Clean, journal-style with serif fonts (default)
  - **Dark**: Dark background with light text
  - **Terminal**: Green text on black with monospace font and glowing effects
  - **Cyberpunk**: Neon magenta/cyan colors with glowing text shadows
- **Floating Theme Selector**: Hover button in top-right corner
- **Responsive Design**: Works beautifully on desktop and mobile
- **Journal Aesthetic**: Typography and spacing designed to feel like a personal journal

### Technical Features
- **ASP.NET Core MVC**: Built with .NET 9
- **Markdown Processing**: Uses Markdig for rich Markdown support
- **Theme Persistence**: User's theme choice saved in localStorage
- **Clean Architecture**: Separation of concerns with services and models

## Getting Started

### Prerequisites
- .NET 9.0 SDK or later
- A modern web browser

### Installation

1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd JournalBlog
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Run the application:
   ```bash
   dotnet run
   ```

4. Open your browser and navigate to `https://localhost:5001` (or the URL shown in the console)

## Adding Blog Posts

1. Create a new `.md` file in the `posts/` folder
2. Name it with the date format: `YYYYMMDD.md` (e.g., `20250619.md`)
3. Add YAML front matter at the top:

```markdown
---
title: Your Post Title
date: 2025-06-19T10:00:00
tags: ["tag1", "tag2", "tag3"]
author: CodeOnARaft
summary: A brief summary of your post for the home page.
---

# Your Post Title

Your markdown content goes here...
```

## Project Structure

```
JournalBlog/
├── Controllers/
│   ├── BlogController.cs      # Main blog functionality
│   └── ThemeController.cs     # Theme management API
├── Models/
│   ├── BlogPost.cs           # Blog post model
│   ├── PagedResult.cs        # Pagination model
│   └── Theme.cs              # Theme models
├── Services/
│   ├── BlogService.cs        # Blog post processing
│   └── ThemeService.cs       # Theme management
├── Views/
│   ├── Blog/                 # Blog views
│   └── Shared/               # Layout and shared views
├── wwwroot/
│   ├── css/
│   │   ├── site.css         # Main styles
│   │   └── themes.css       # Theme definitions
│   └── js/
│       └── themes.js        # Theme switching logic
└── posts/                   # Markdown blog posts
```

## Customization

### Adding New Themes
1. Add theme variables to `wwwroot/css/themes.css`
2. Update the theme icons and names in `ThemeService.cs`
3. Add the theme option to the layout

### Styling
- Main styles are in `wwwroot/css/site.css`
- Theme variables are defined in `wwwroot/css/themes.css`
- All colors use CSS custom properties for easy theming

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test thoroughly
5. Submit a pull request

## License

This project is open source and available under the [MIT License](LICENSE).

## About

Created by CodeOnARaft as a simple, elegant blogging platform that prioritizes the writing experience and reader engagement over complex features.

---

*"Sometimes the most powerful tool is the simplest one."*