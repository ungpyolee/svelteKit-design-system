// src/routes/+page.server.js
import { redirect } from '@sveltejs/kit';

export function load() {
    // 307: 임시 리다이렉트, 308: 영구 리다이렉트
    // 보통 개발 시에는 수정이 용이한 307을 자주 사용합니다.
    throw redirect(307, '/dashboard');
}