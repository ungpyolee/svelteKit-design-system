import { writable } from 'svelte/store';
import { browser } from '$app/environment';

/**
 * 초기 테마 결정
 */
function getInitialTheme() {
  if (!browser) return 'light';

  const stored = localStorage.getItem('theme');
  if (stored === 'dark' || stored === 'light') {
    return stored;
  }

  if (window.matchMedia('(prefers-color-scheme: dark)').matches) {
    return 'dark';
  }

  return 'light';
}

/**
 * 테마 스토어 생성
 */
function createThemeStore() {
  const { subscribe, set, update } = writable(getInitialTheme());

  return {
    subscribe,

    /**
     * 테마 설정
     * @param {'light' | 'dark'} newTheme
     */
    setTheme: (newTheme) => {
      if (!browser) return;

      localStorage.setItem('theme', newTheme);
      document.documentElement.classList.toggle('dark', newTheme === 'dark');
      set(newTheme);
    },

    /**
     * 테마 토글
     */
    toggle: () => {
      update((current) => {
        const newTheme = current === 'dark' ? 'light' : 'dark';

        if (browser) {
          localStorage.setItem('theme', newTheme);
          document.documentElement.classList.toggle('dark', newTheme === 'dark');
        }

        return newTheme;
      });
    },

    /**
     * 시스템 테마 사용
     */
    useSystem: () => {
      if (!browser) return;

      localStorage.removeItem('theme');
      const systemDark = window.matchMedia('(prefers-color-scheme: dark)').matches;
      document.documentElement.classList.toggle('dark', systemDark);
      set(systemDark ? 'dark' : 'light');
    },

    /**
     * 초기화 (앱 시작 시 호출)
     */
    init: () => {
      if (!browser) return;

      const currentTheme = getInitialTheme();
      document.documentElement.classList.toggle('dark', currentTheme === 'dark');
      set(currentTheme);
    },
  };
}

export const theme = createThemeStore();