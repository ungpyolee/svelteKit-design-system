/** @type {import('tailwindcss').Config} */
export default {
  content: ['./src/**/*.{html,js,svelte}'],
  darkMode: 'class',
  theme: {
    extend: {
       fontFamily: {
        sans: ['Pretendard', 'sans-serif'],
        // en: ['Edu', 'sans-serif'],
      },
      colors: {
        background: 'var(--color-background)',
        foreground: 'var(--color-foreground)',
        primary: {
          DEFAULT: 'var(--color-primary)',
          foreground: 'var(--color-primary-foreground)',
        },
        secondary: {
          DEFAULT: 'var(--color-secondary)',
          foreground: 'var(--color-secondary-foreground)',
        },
        muted: {
          DEFAULT: 'var(--color-muted)',
          foreground: 'var(--color-muted-foreground)',
        },
        accent: {
          DEFAULT: 'var(--color-accent)',
          foreground: 'var(--color-accent-foreground)',
        },
        destructive: {
          DEFAULT: 'var(--color-destructive)',
          foreground: 'var(--color-destructive-foreground)',
        },
        gray: {
          50: '#F3F4F8',
          100: '#EAECEF',
          200: '#D4D5D9',
          300: '#BCBDC3',
          400: '#9D9EA4',
          500: '#78797D',
          600: '#606166',
          700: '#48494E',
          800: '#2F3033',
          900: '#1C1C1F',
          950: '#0F0F11'
        },
        border: 'var(--color-border)',
        input: 'var(--color-input)',
        ring: 'var(--color-ring)',
      },
      borderRadius: {
        lg: 'var(--radius)',
        md: 'calc(var(--radius) - 2px)',
        sm: 'calc(var(--radius) - 4px)',
      },
    },
  },
  plugins: [
    require('@tailwindcss/forms'),
    require('@tailwindcss/typography'),
  ],
}
