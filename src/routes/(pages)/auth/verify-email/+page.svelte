<!-- /routes/(pages)/auth/verify-email/+page.svelte -->
<script>
    import { page } from '$app/stores';
    import { goto } from '$app/navigation';
    import { onMount } from 'svelte';
    import Icon from "$lib/icons/icon.svelte";

    let email = $derived($page.url.searchParams.get('email') || '');
    let token = $derived($page.url.searchParams.get('token'));
    let isVerifying = $state(false);
    let isVerified = $state(false);
    let isResending = $state(false);
    let resendSuccess = $state(false);
    let error = $state('');

    onMount(async () => {
        // 토큰이 있으면 자동 인증 시도
        if (token) {
            await verifyEmail();
        }
    });

    async function verifyEmail() {
        isVerifying = true;
        error = '';

        try {
            await new Promise(resolve => setTimeout(resolve, 1500));
            console.log('Verify email:', token);
            isVerified = true;
        } catch (err) {
            error = '이메일 인증에 실패했습니다. 링크가 만료되었을 수 있습니다.';
        } finally {
            isVerifying = false;
        }
    }

    async function resendEmail() {
        isResending = true;
        resendSuccess = false;

        try {
            await new Promise(resolve => setTimeout(resolve, 1000));
            console.log('Resend verification email:', email);
            resendSuccess = true;
        } catch (err) {
            error = '이메일 발송에 실패했습니다.';
        } finally {
            isResending = false;
        }
    }
</script>

<svelte:head>
    <title>이메일 인증 - RMS</title>
</svelte:head>

<div class="space-y-6 text-center">
    {#if isVerifying}
        <!-- 인증 중 -->
        <div class="space-y-4">
            <div class="w-16 h-16 mx-auto bg-primary-100 dark:bg-primary-900 rounded-full flex items-center justify-center">
                <Icon name="Loader2" size="lg" class="text-primary animate-spin" />
            </div>
            <div>
                <h1 class="text-2xl font-bold text-gray-900 dark:text-white">이메일 인증 중...</h1>
                <p class="mt-2 text-sm text-gray-600 dark:text-gray-400">
                    잠시만 기다려주세요.
                </p>
            </div>
        </div>
    {:else if isVerified}
        <!-- 인증 완료 -->
        <div class="space-y-4">
            <div class="w-16 h-16 mx-auto bg-green-100 dark:bg-green-900 rounded-full flex items-center justify-center">
                <Icon name="CheckCircle" size="lg" class="text-green-600 dark:text-green-400" />
            </div>
            <div>
                <h1 class="text-2xl font-bold text-gray-900 dark:text-white">이메일 인증 완료!</h1>
                <p class="mt-2 text-sm text-gray-600 dark:text-gray-400">
                    회원가입이 완료되었습니다.<br/>
                    지금 바로 로그인하세요.
                </p>
            </div>
            <button
                onclick={() => goto('/auth/login')}
                class="w-full py-2.5 bg-primary text-white font-medium rounded-lg hover:bg-primary-600 transition-colors"
            >
                로그인하기
            </button>
        </div>
    {:else if error}
        <!-- 에러 -->
        <div class="space-y-4">
            <div class="w-16 h-16 mx-auto bg-red-100 dark:bg-red-900 rounded-full flex items-center justify-center">
                <Icon name="AlertTriangle" size="lg" class="text-red-600 dark:text-red-400" />
            </div>
            <div>
                <h1 class="text-2xl font-bold text-gray-900 dark:text-white">인증 실패</h1>
                <p class="mt-2 text-sm text-gray-600 dark:text-gray-400">{error}</p>
            </div>
            <button
                onclick={resendEmail}
                disabled={isResending}
                class="w-full py-2.5 bg-primary text-white font-medium rounded-lg hover:bg-primary-600 disabled:opacity-50 transition-colors flex items-center justify-center gap-2"
            >
                {#if isResending}
                    <Icon name="Loader2" size="sm" class="animate-spin" />
                    발송 중...
                {:else}
                    인증 이메일 다시 받기
                {/if}
            </button>
        </div>
    {:else}
        <!-- 이메일 확인 요청 -->
        <div class="space-y-4">
            <div class="w-16 h-16 mx-auto bg-blue-100 dark:bg-blue-900 rounded-full flex items-center justify-center">
                <Icon name="Mail" size="lg" class="text-blue-600 dark:text-blue-400" />
            </div>
            <div>
                <h1 class="text-2xl font-bold text-gray-900 dark:text-white">이메일을 확인해주세요</h1>
                <p class="mt-2 text-sm text-gray-600 dark:text-gray-400">
                    {#if email}
                        <span class="font-medium text-gray-800 dark:text-gray-200">{email}</span>로<br/>
                    {/if}
                    인증 링크를 발송했습니다.<br/>
                    이메일의 링크를 클릭하여 가입을 완료해주세요.
                </p>
            </div>

            {#if resendSuccess}
                <div class="p-3 bg-green-50 dark:bg-green-900/20 border border-green-200 dark:border-green-800 rounded-lg">
                    <p class="text-sm text-green-600 dark:text-green-400">인증 이메일을 다시 발송했습니다.</p>
                </div>
            {/if}

            <div class="pt-4 space-y-3">
                <p class="text-xs text-gray-500 dark:text-gray-400">
                    이메일이 도착하지 않았나요?
                </p>
                <button
                    onclick={resendEmail}
                    disabled={isResending}
                    class="text-sm text-primary hover:text-primary-600 font-medium disabled:opacity-50 flex items-center justify-center gap-2 mx-auto"
                >
                    {#if isResending}
                        <Icon name="Loader2" size="sm" class="animate-spin" />
                        발송 중...
                    {:else}
                        <Icon name="RefreshCw" size="sm" />
                        인증 이메일 다시 받기
                    {/if}
                </button>
            </div>

            <div class="pt-4 border-t border-gray-200 dark:border-gray-700">
                <a href="/auth/login" class="text-sm text-gray-500 dark:text-gray-400 hover:text-primary flex items-center justify-center gap-1">
                    <Icon name="ArrowLeft" size="sm" />
                    로그인으로 돌아가기
                </a>
            </div>
        </div>
    {/if}
</div>