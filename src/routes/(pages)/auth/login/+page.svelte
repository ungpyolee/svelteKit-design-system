<!-- /routes/(pages)/auth/login/+page.svelte -->
<script>
    import { goto } from '$app/navigation';
    import Icon from "$lib/icons/icon.svelte";

    let email = $state('');
    let password = $state('');
    let rememberMe = $state(false);
    let showPassword = $state(false);
    let isLoading = $state(false);
    let error = $state('');

    async function handleSubmit(e) {
        e.preventDefault();
        error = '';
        
        if (!email || !password) {
            error = '이메일과 비밀번호를 입력해주세요.';
            return;
        }

        isLoading = true;
        
        try {
            // API 호출
            await new Promise(resolve => setTimeout(resolve, 1000));
            console.log('Login:', { email, password, rememberMe });
            goto('/rms/user-dashboard');
        } catch (err) {
            error = '로그인에 실패했습니다. 이메일과 비밀번호를 확인해주세요.';
        } finally {
            isLoading = false;
        }
    }

    function handleGoogleLogin() {
        console.log('Google login');
        // OAuth 리다이렉트
    }
</script>

<svelte:head>
    <title>로그인 - RMS</title>
</svelte:head>

<div class="space-y-6">
    <!-- 헤더 -->
    <div class="text-center lg:text-left">
        <h1 class="text-2xl font-bold text-gray-900 dark:text-white">로그인</h1>
        <p class="mt-2 text-sm text-gray-600 dark:text-gray-400">
            계정이 없으신가요?
            <a href="/auth/register" class="text-primary hover:text-primary-600 font-medium">회원가입</a>
        </p>
    </div>

    <!-- 에러 메시지 -->
    {#if error}
        <div class="p-4 bg-red-50 dark:bg-red-900/20 border border-red-200 dark:border-red-800 rounded-lg flex items-start gap-3">
            <Icon name="AlertCircle" size="sm" class="text-red-500 flex-shrink-0 mt-0.5" />
            <p class="text-sm text-red-600 dark:text-red-400">{error}</p>
        </div>
    {/if}

    <!-- 소셜 로그인 -->
    <button
        onclick={handleGoogleLogin}
        class="w-full flex items-center justify-center gap-3 px-4 py-3 border border-gray-200 dark:border-gray-700 rounded-lg hover:bg-gray-50 dark:hover:bg-gray-800 transition-colors"
    >
        <svg class="w-5 h-5" viewBox="0 0 24 24">
            <path fill="#4285F4" d="M22.56 12.25c0-.78-.07-1.53-.2-2.25H12v4.26h5.92c-.26 1.37-1.04 2.53-2.21 3.31v2.77h3.57c2.08-1.92 3.28-4.74 3.28-8.09z"/>
            <path fill="#34A853" d="M12 23c2.97 0 5.46-.98 7.28-2.66l-3.57-2.77c-.98.66-2.23 1.06-3.71 1.06-2.86 0-5.29-1.93-6.16-4.53H2.18v2.84C3.99 20.53 7.7 23 12 23z"/>
            <path fill="#FBBC05" d="M5.84 14.09c-.22-.66-.35-1.36-.35-2.09s.13-1.43.35-2.09V7.07H2.18C1.43 8.55 1 10.22 1 12s.43 3.45 1.18 4.93l2.85-2.22.81-.62z"/>
            <path fill="#EA4335" d="M12 5.38c1.62 0 3.06.56 4.21 1.64l3.15-3.15C17.45 2.09 14.97 1 12 1 7.7 1 3.99 3.47 2.18 7.07l3.66 2.84c.87-2.6 3.3-4.53 6.16-4.53z"/>
        </svg>
        <span class="text-sm font-medium text-gray-700 dark:text-gray-300">Google로 계속하기</span>
    </button>

    <!-- 구분선 -->
    <div class="relative">
        <div class="absolute inset-0 flex items-center">
            <div class="w-full border-t border-gray-200 dark:border-gray-700"></div>
        </div>
        <div class="relative flex justify-center text-sm">
            <span class="px-4 bg-gray-50 dark:bg-gray-950 text-gray-500 dark:text-gray-400">또는</span>
        </div>
    </div>

    <!-- 로그인 폼 -->
    <form onsubmit={handleSubmit} class="space-y-4">
        <!-- 이메일 -->
        <div>
            <label for="email" class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                이메일
            </label>
            <div class="relative">
                <Icon name="Mail" size="sm" class="absolute left-3 top-1/2 -translate-y-1/2 text-gray-400" />
                <input
                    type="email"
                    id="email"
                    bind:value={email}
                    placeholder="name@company.com"
                    class="w-full pl-10 pr-4 py-2.5 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 placeholder-gray-400 focus:ring-2 focus:ring-primary focus:border-transparent outline-none transition-all"
                />
            </div>
        </div>

        <!-- 비밀번호 -->
        <div>
            <label for="password" class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                비밀번호
            </label>
            <div class="relative">
                <Icon name="Lock" size="sm" class="absolute left-3 top-1/2 -translate-y-1/2 text-gray-400" />
                <input
                    type={showPassword ? 'text' : 'password'}
                    id="password"
                    bind:value={password}
                    placeholder="••••••••"
                    class="w-full pl-10 pr-12 py-2.5 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 placeholder-gray-400 focus:ring-2 focus:ring-primary focus:border-transparent outline-none transition-all"
                />
                <button
                    type="button"
                    onclick={() => showPassword = !showPassword}
                    class="absolute right-3 top-1/2 -translate-y-1/2 text-gray-400 hover:text-gray-600 dark:hover:text-gray-300"
                >
                    <Icon name={showPassword ? 'EyeOff' : 'Eye'} size="sm" />
                </button>
            </div>
        </div>

        <!-- 옵션 -->
        <div class="flex items-center justify-between">
            <label class="flex items-center gap-2 cursor-pointer">
                <input
                    type="checkbox"
                    bind:checked={rememberMe}
                    class="w-4 h-4 rounded border-gray-300 dark:border-gray-600 text-primary focus:ring-primary"
                />
                <span class="text-sm text-gray-600 dark:text-gray-400">로그인 상태 유지</span>
            </label>
            <a href="/auth/forgot-password" class="text-sm text-primary hover:text-primary-600 font-medium">
                비밀번호 찾기
            </a>
        </div>

        <!-- 제출 버튼 -->
        <button
            type="submit"
            disabled={isLoading}
            class="w-full py-2.5 bg-primary text-white font-medium rounded-lg hover:bg-primary-600 disabled:opacity-50 disabled:cursor-not-allowed transition-colors flex items-center justify-center gap-2"
        >
            {#if isLoading}
                <Icon name="Loader2" size="sm" class="animate-spin" />
                로그인 중...
            {:else}
                로그인
            {/if}
        </button>
    </form>
</div>