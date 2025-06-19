// Theme Management
class ThemeManager {
    constructor() {
        this.currentTheme = this.getStoredTheme() || 'light';
        this.themeOptions = document.querySelectorAll('.theme-option');
        this.currentThemeButton = document.getElementById('current-theme-button');
        this.currentThemeIcon = document.querySelector('.current-theme-icon');
        this.body = document.body;
        
        this.themeIcons = {
            light: '☀️',
            dark: '🌙',
            terminal: '💻',
            cyberpunk: '🌆'
        };
        
        this.init();
    }

    init() {
        // Apply stored theme on load
        this.applyTheme(this.currentTheme);
        
        // Set up event listeners
        this.themeOptions.forEach(option => {
            option.addEventListener('click', (e) => {
                const theme = e.currentTarget.dataset.theme;
                this.switchTheme(theme);
            });
        });
        
        // Update UI
        this.updateUI();
    }

    switchTheme(newTheme) {
        if (newTheme === this.currentTheme) return;
        
        this.currentTheme = newTheme;
        this.applyTheme(newTheme);
        this.storeTheme(newTheme);
        this.updateUI();
    }

    applyTheme(theme) {
        // Remove all theme classes
        this.body.classList.remove('theme-light', 'theme-dark', 'theme-terminal', 'theme-cyberpunk');
        
        // Add new theme class
        this.body.classList.add(`theme-${theme}`);
        
        // Special handling for terminal and cyberpunk themes
        if (theme === 'terminal' || theme === 'cyberpunk') {
            this.body.style.fontFamily = theme === 'terminal' ? 
                "'Courier New', monospace" : 
                "'Crimson Text', 'Georgia', serif";
        } else {
            this.body.style.fontFamily = "'Crimson Text', 'Georgia', serif";
        }
    }

    updateUI() {
        // Update current theme icon
        if (this.currentThemeIcon) {
            this.currentThemeIcon.textContent = this.themeIcons[this.currentTheme];
        }
        
        // Update active state in dropdown
        this.themeOptions.forEach(option => {
            option.classList.remove('active');
            if (option.dataset.theme === this.currentTheme) {
                option.classList.add('active');
            }
        });
    }

    storeTheme(theme) {
        try {
            localStorage.setItem('journal-theme', theme);
        } catch (e) {
            console.warn('Could not save theme preference:', e);
        }
    }

    getStoredTheme() {
        try {
            return localStorage.getItem('journal-theme');
        } catch (e) {
            console.warn('Could not retrieve theme preference:', e);
            return null;
        }
    }
}

// Initialize theme manager when DOM is loaded
document.addEventListener('DOMContentLoaded', () => {
    new ThemeManager();
});

// Export for potential external use
window.ThemeManager = ThemeManager;