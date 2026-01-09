import { writable } from 'svelte/store';
import { browser } from '$app/environment';

/**
 * 초기 상태 결정
 */
function getInitialState() {
  if (!browser) return { expanded: true };

  const stored = localStorage.getItem('sidebar');
  if (stored) {
    try {
      return JSON.parse(stored);
    } catch {
      return { expanded: true };
    }
  }

  return { expanded: true };
}

/**
 * Sidebar 스토어 생성
 */
function createSidebarStore() {
  const { subscribe, set, update } = writable(getInitialState());

  return {
    subscribe,

    /**
     * 펼침/접힘 토글
     */
    toggle: () => {
      update((state) => {
        const newState = { ...state, expanded: !state.expanded };

        if (browser) {
          localStorage.setItem('sidebar', JSON.stringify(newState));
        }

        return newState;
      });
    },

    /**
     * 펼침 상태 설정
     * @param {boolean} expanded
     */
    setExpanded: (expanded) => {
      update((state) => {
        const newState = { ...state, expanded };

        if (browser) {
          localStorage.setItem('sidebar', JSON.stringify(newState));
        }

        return newState;
      });
    },

    /**
     * 초기화
     */
    init: () => {
      if (!browser) return;
      set(getInitialState());
    },
  };
}

export const sidebar = createSidebarStore();