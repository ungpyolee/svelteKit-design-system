<!-- /routes/(pages)/auth/callback/+page.svelte -->
<script>
    import { page } from '$app/stores';
    import { goto } from '$app/navigation';
    import { onMount } from 'svelte';
    import Icon from "$lib/icons/icon.svelte";

    let status = $state('loading'); // loading, success, error
    let error = $state('');

    // URL 파라미터
    let code = $derived($page.url.searchParams.get('code'));
    let provider = $derived($page.url.searchParams.get('provider') || 'google');
    let errorParam = $derived($page.url.searchParams.get('error'));

    onMount(async () => {
        if (errorParam) {
            status = 'error';
            error = '소셜 로그인이 취소되었습니다.';
            return;
        }

        if (!code) {
            status = 'error';
            error = '잘못된 접근입니다.';
            return;
        }

        try {
            // OAuth 토큰 교환 및 로그인 처리
            await new Promise(resolve => setTimeout(resolve, 2000));
            console.log('OAuth callback:', { code, provider });
            
            status = 'success';
            
            // 잠시 후 대시보드로 이동
            setTimeout(() => {
                goto('/rms/user-dashboard');
            }, 1500);
        } catch (err) {
            status = 'error';
            error = '로그인 처리 중 오류가 발생했습니다.';
        }
    });
</script>

<svelte:head>
    <title>로그인 처리 중 - RMS</title>
</svelte:head>

<div class="space-y-6 text-center">
    {#if status === 'loading'}
        <div class="space-y-4">
            <div class="w-16 h-16 mx-auto bg-primary-100 dark:bg-primary-900 rounded-full flex items-center justify-center">
                <Icon name="Loader2" size="lg" class="text-primary animate-spin" />
            </div>
            <div>
                <h1 class="text-2xl font-bold text-gray-900 dark:text-white">로그인 처리 중...</h1>
                <p class="mt-2 text-sm text-gray-600 dark:text-gray-400">
                    잠시만 기다려주세요.
                </p>
            </div>
        </div>
    {:else if status === 'success'}
        <div class="space-y-4">
            <div class="w-16 h-16 mx-auto bg-green-100 dark:bg-green-900 rounded-full flex items-center justify-center">
                <Icon name="CheckCircle" size="lg" class="text-green-600 dark:text-green-400" />
            </div>
            <div>
                <h1 class="text-2xl font-bold text-gray-900 dark:text-white">로그인 성공!</h1>
                <p class="mt-2 text-sm text-gray-600 dark:text-gray-400">
                    잠시 후 자동으로 이동합니다.
                </p>
            </div>
        </div>
    {:else}
        <div class="space-y-4">
            <div class="w-16 h-16 mx-auto bg-red-100 dark:bg-red-900 rounded-full flex items-center justify-center">
                <Icon name="AlertTriangle" size="lg" class="text-red-600 dark:text-red-400" />
            </div>
            <div>
                <h1 class="text-2xl font-bold text-gray-900 dark:text-white">로그인 실패</h1>
                <p class="mt-2 text-sm text-gray-600 dark:text-gray-400">{error}</p>
            </div>
            <a
                href="/auth/login"
                class="inline-block w-full py-2.5 bg-primary text-white font-medium rounded-lg hover:bg-primary-600 transition-colors text-center"
            >
                다시 시도
            </a>
        </div>
    {/if}
</div>