import { clsx } from 'clsx';
import { twMerge } from 'tailwind-merge';

/**
 * 클래스명 병합 유틸리티
 * Tailwind 클래스 충돌을 자동으로 해결
 * @param {...(string|object|array)} inputs
 * @returns {string}
 */
export function cn(...inputs) {
  return twMerge(clsx(inputs));
}