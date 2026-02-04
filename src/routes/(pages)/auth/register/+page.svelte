<!-- /routes/(pages)/auth/register/+page.svelte -->
<script>
    import { goto } from '$app/navigation';
    import Icon from "$lib/icons/icon.svelte";

    let name = $state('');
    let email = $state('');
    let password = $state('');
    let passwordConfirm = $state('');
    let showPassword = $state(false);
    let agreeTerms = $state(false);
    let agreePrivacy = $state(false);
    let agreeMarketing = $state(false);
    let isLoading = $state(false);
    let error = $state('');

    // 비밀번호 유효성 검사
    let passwordValidation = $derived({
        minLength: password.length >= 8,
        hasNumber: /\d/.test(password),
        hasLetter: /[a-zA-Z]/.test(password),
        hasSpecial: /[!@#$%^&*(),.?":{}|<>]/.test(password),
    });

    let isPasswordValid = $derived(
        passwordValidation.minLength && 
        passwordValidation.hasNumber && 
        passwordValidation.hasLetter
    );

    let passwordsMatch = $derived(password === passwordConfirm && passwordConfirm.length > 0);

    async function handleSubmit(e) {
        e.preventDefault();
        error = '';

        if (!name || !email || !password || !passwordConfirm) {
            error = '모든 필수 항목을 입력해주세요.';
            return;
        }

        if (!isPasswordValid) {
            error = '비밀번호 조건을 확인해주세요.';
            return;
        }

        if (!passwordsMatch) {
            error = '비밀번호가 일치하지 않습니다.';
            return;
        }

        if (!agreeTerms || !agreePrivacy) {
            error = '필수 약관에 동의해주세요.';
            return;
        }

        isLoading = true;

        try {
            await new Promise(resolve => setTimeout(resolve, 1000));
            console.log('Register:', { name, email, password, agreeMarketing });
            goto('/auth/verify-email?email=' + encodeURIComponent(email));
        } catch (err) {
            error = '회원가입에 실패했습니다. 다시 시도해주세요.';
        } finally {
            isLoading = false;
        }
    }

    function handleGoogleLogin() {
        console.log('Google signup');
    }

    function toggleAllAgree() {
        const allChecked = agreeTerms && agreePrivacy && agreeMarketing;
        agreeTerms = !allChecked;
        agreePrivacy = !allChecked;
        agreeMarketing = !allChecked;
    }
</script>

<svelte:head>
    <title>회원가입 - RMS</title>
</svelte:head>

<div class="space-y-6">
    <!-- 헤더 -->
    <div class="text-center lg:text-left">
        <h1 class="text-2xl font-bold text-gray-900 dark:text-white">회원가입</h1>
        <p class="mt-2 text-sm text-gray-600 dark:text-gray-400">
            이미 계정이 있으신가요?
            <a href="/auth/login" class="text-primary hover:text-primary-600 font-medium">로그인</a>
        </p>
    </div>

    <!-- 에러 메시지 -->
    {#if error}
        <div class="p-4 bg-red-50 dark:bg-red-900/20 border border-red-200 dark:border-red-800 rounded-lg flex items-start gap-3">
            <Icon name="AlertCircle" size="sm" class="text-red-500 flex-shrink-0 mt-0.5" />
            <p class="text-sm text-red-600 dark:text-red-400">{error}</p>
        </div>
    {/if}

    <!-- 소셜 가입 -->
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
        <span class="text-sm font-medium text-gray-700 dark:text-gray-300">Google로 가입하기</span>
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

    <!-- 회원가입 폼 -->
    <form onsubmit={handleSubmit} class="space-y-4">
        <!-- 이름 -->
        <div>
            <label for="name" class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                이름 <span class="text-red-500">*</span>
            </label>
            <div class="relative">
                <Icon name="User" size="sm" class="absolute left-3 top-1/2 -translate-y-1/2 text-gray-400" />
                <input
                    type="text"
                    id="name"
                    bind:value={name}
                    placeholder="홍길동"
                    class="w-full pl-10 pr-4 py-2.5 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 placeholder-gray-400 focus:ring-2 focus:ring-primary focus:border-transparent outline-none transition-all"
                />
            </div>
        </div>

        <!-- 이메일 -->
        <div>
            <label for="email" class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                이메일 <span class="text-red-500">*</span>
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
                비밀번호 <span class="text-red-500">*</span>
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
            <!-- 비밀번호 조건 -->
            {#if password.length > 0}
                <div class="mt-2 space-y-1">
                    <div class="flex items-center gap-2 text-xs {passwordValidation.minLength ? 'text-green-600 dark:text-green-400' : 'text-gray-400'}">
                        <Icon name={passwordValidation.minLength ? 'Check' : 'X'} size="xs" />
                        8자 이상
                    </div>
                    <div class="flex items-center gap-2 text-xs {passwordValidation.hasLetter ? 'text-green-600 dark:text-green-400' : 'text-gray-400'}">
                        <Icon name={passwordValidation.hasLetter ? 'Check' : 'X'} size="xs" />
                        영문 포함
                    </div>
                    <div class="flex items-center gap-2 text-xs {passwordValidation.hasNumber ? 'text-green-600 dark:text-green-400' : 'text-gray-400'}">
                        <Icon name={passwordValidation.hasNumber ? 'Check' : 'X'} size="xs" />
                        숫자 포함
                    </div>
                </div>
            {/if}
        </div>

        <!-- 비밀번호 확인 -->
        <div>
            <label for="passwordConfirm" class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                비밀번호 확인 <span class="text-red-500">*</span>
            </label>
            <div class="relative">
                <Icon name="Lock" size="sm" class="absolute left-3 top-1/2 -translate-y-1/2 text-gray-400" />
                <input
                    type={showPassword ? 'text' : 'password'}
                    id="passwordConfirm"
                    bind:value={passwordConfirm}
                    placeholder="••••••••"
                    class="w-full pl-10 pr-12 py-2.5 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 placeholder-gray-400 focus:ring-2 focus:ring-primary focus:border-transparent outline-none transition-all {passwordConfirm.length > 0 && !passwordsMatch ? 'border-red-500 focus:ring-red-500' : ''}"
                />
                {#if passwordConfirm.length > 0}
                    <div class="absolute right-3 top-1/2 -translate-y-1/2">
                        {#if passwordsMatch}
                            <Icon name="CheckCircle" size="sm" class="text-green-500" />
                        {:else}
                            <Icon name="XCircle" size="sm" class="text-red-500" />
                        {/if}
                    </div>
                {/if}
            </div>
            {#if passwordConfirm.length > 0 && !passwordsMatch}
                <p class="mt-1 text-xs text-red-500">비밀번호가 일치하지 않습니다.</p>
            {/if}
        </div>

        <!-- 약관 동의 -->
        <div class="space-y-3 pt-2">
            <label class="flex items-center gap-3 cursor-pointer">
                <input
                    type="checkbox"
                    checked={agreeTerms && agreePrivacy && agreeMarketing}
                    onchange={toggleAllAgree}
                    class="w-5 h-5 rounded border-gray-300 dark:border-gray-600 text-primary focus:ring-primary"
                />
                <span class="text-sm font-medium text-gray-800 dark:text-gray-200">전체 동의</span>
            </label>
            
            <div class="pl-8 space-y-2">
                <label class="flex items-center gap-3 cursor-pointer">
                    <input
                        type="checkbox"
                        bind:checked={agreeTerms}
                        class="w-4 h-4 rounded border-gray-300 dark:border-gray-600 text-primary focus:ring-primary"
                    />
                    <span class="text-sm text-gray-600 dark:text-gray-400">
                        <span class="text-red-500">(필수)</span> 
                        <a href="/terms" class="underline hover:text-primary">이용약관</a> 동의
                    </span>
                </label>
                <label class="flex items-center gap-3 cursor-pointer">
                    <input
                        type="checkbox"
                        bind:checked={agreePrivacy}
                        class="w-4 h-4 rounded border-gray-300 dark:border-gray-600 text-primary focus:ring-primary"
                    />
                    <span class="text-sm text-gray-600 dark:text-gray-400">
                        <span class="text-red-500">(필수)</span>
                        <a href="/privacy" class="underline hover:text-primary">개인정보처리방침</a> 동의
                    </span>
                </label>
                <label class="flex items-center gap-3 cursor-pointer">
                    <input
                        type="checkbox"
                        bind:checked={agreeMarketing}
                        class="w-4 h-4 rounded border-gray-300 dark:border-gray-600 text-primary focus:ring-primary"
                    />
                    <span class="text-sm text-gray-600 dark:text-gray-400">
                        (선택) 마케팅 정보 수신 동의
                    </span>
                </label>
            </div>
        </div>

        <!-- 제출 버튼 -->
        <button
            type="submit"
            disabled={isLoading}
            class="w-full py-2.5 bg-primary text-white font-medium rounded-lg hover:bg-primary-600 disabled:opacity-50 disabled:cursor-not-allowed transition-colors flex items-center justify-center gap-2"
        >
            {#if isLoading}
                <Icon name="Loader2" size="sm" class="animate-spin" />
                가입 중...
            {:else}
                회원가입
            {/if}
        </button>
    </form>
</div>