<!-- /routes/(pages)/auth/reset-password/+page.svelte -->
<script>
    import { page } from '$app/stores';
    import { goto } from '$app/navigation';
    import Icon from "$lib/icons/icon.svelte";

    let password = $state('');
    let passwordConfirm = $state('');
    let showPassword = $state(false);
    let isLoading = $state(false);
    let isSuccess = $state(false);
    let error = $state('');

    // URL에서 토큰 가져오기
    let token = $derived($page.url.searchParams.get('token'));

    let passwordValidation = $derived({
        minLength: password.length >= 8,
        hasNumber: /\d/.test(password),
        hasLetter: /[a-zA-Z]/.test(password),
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

        if (!isPasswordValid) {
            error = '비밀번호 조건을 확인해주세요.';
            return;
        }

        if (!passwordsMatch) {
            error = '비밀번호가 일치하지 않습니다.';
            return;
        }

        isLoading = true;

        try {
            await new Promise(resolve => setTimeout(resolve, 1000));
            console.log('Reset password:', { token, password });
            isSuccess = true;
        } catch (err) {
            error = '비밀번호 재설정에 실패했습니다. 링크가 만료되었을 수 있습니다.';
        } finally {
            isLoading = false;
        }
    }
</script>

<svelte:head>
    <title>비밀번호 재설정 - RMS</title>
</svelte:head>

<div class="space-y-6">
    {#if isSuccess}
        <!-- 성공 -->
        <div class="text-center space-y-4">
            <div class="w-16 h-16 mx-auto bg-green-100 dark:bg-green-900 rounded-full flex items-center justify-center">
                <Icon name="CheckCircle" size="lg" class="text-green-600 dark:text-green-400" />
            </div>
            <div>
                <h1 class="text-2xl font-bold text-gray-900 dark:text-white">비밀번호가 변경되었습니다</h1>
                <p class="mt-2 text-sm text-gray-600 dark:text-gray-400">
                    새로운 비밀번호로 로그인해주세요.
                </p>
            </div>
            <button
                onclick={() => goto('/auth/login')}
                class="w-full py-2.5 bg-primary text-white font-medium rounded-lg hover:bg-primary-600 transition-colors"
            >
                로그인하기
            </button>
        </div>
    {:else if !token}
        <!-- 토큰 없음 -->
        <div class="text-center space-y-4">
            <div class="w-16 h-16 mx-auto bg-red-100 dark:bg-red-900 rounded-full flex items-center justify-center">
                <Icon name="AlertTriangle" size="lg" class="text-red-600 dark:text-red-400" />
            </div>
            <div>
                <h1 class="text-2xl font-bold text-gray-900 dark:text-white">잘못된 접근입니다</h1>
                <p class="mt-2 text-sm text-gray-600 dark:text-gray-400">
                    유효하지 않은 링크입니다.<br/>
                    비밀번호 찾기를 다시 시도해주세요.
                </p>
            </div>
            <a
                href="/auth/forgot-password"
                class="inline-block w-full py-2.5 bg-primary text-white font-medium rounded-lg hover:bg-primary-600 transition-colors text-center"
            >
                비밀번호 찾기
            </a>
        </div>
    {:else}
        <!-- 재설정 폼 -->
        <div class="text-center lg:text-left">
            <h1 class="text-2xl font-bold text-gray-900 dark:text-white">새 비밀번호 설정</h1>
            <p class="mt-2 text-sm text-gray-600 dark:text-gray-400">
                새로운 비밀번호를 입력해주세요.
            </p>
        </div>

        {#if error}
            <div class="p-4 bg-red-50 dark:bg-red-900/20 border border-red-200 dark:border-red-800 rounded-lg flex items-start gap-3">
                <Icon name="AlertCircle" size="sm" class="text-red-500 flex-shrink-0 mt-0.5" />
                <p class="text-sm text-red-600 dark:text-red-400">{error}</p>
            </div>
        {/if}

        <form onsubmit={handleSubmit} class="space-y-4">
            <!-- 새 비밀번호 -->
            <div>
                <label for="password" class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1.5">
                    새 비밀번호
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
                    비밀번호 확인
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
            </div>

            <button
                type="submit"
                disabled={isLoading}
                class="w-full py-2.5 bg-primary text-white font-medium rounded-lg hover:bg-primary-600 disabled:opacity-50 disabled:cursor-not-allowed transition-colors flex items-center justify-center gap-2"
            >
                {#if isLoading}
                    <Icon name="Loader2" size="sm" class="animate-spin" />
                    변경 중...
                {:else}
                    비밀번호 변경
                {/if}
            </button>
        </form>
    {/if}
</div>