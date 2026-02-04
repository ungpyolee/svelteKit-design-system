<!-- /routes/(pages)/auth/forgot-password/+page.svelte -->
<script>
    import Icon from "$lib/icons/icon.svelte";

    let email = $state('');
    let isLoading = $state(false);
    let isSubmitted = $state(false);
    let error = $state('');

    async function handleSubmit(e) {
        e.preventDefault();
        error = '';

        if (!email) {
            error = '이메일을 입력해주세요.';
            return;
        }

        isLoading = true;

        try {
            await new Promise(resolve => setTimeout(resolve, 1000));
            console.log('Forgot password:', email);
            isSubmitted = true;
        } catch (err) {
            error = '요청 처리에 실패했습니다. 다시 시도해주세요.';
        } finally {
            isLoading = false;
        }
    }
</script>

<svelte:head>
    <title>비밀번호 찾기 - RMS</title>
</svelte:head>

<div class="space-y-6">
    {#if isSubmitted}
        <!-- 전송 완료 -->
        <div class="text-center space-y-4">
            <div class="w-16 h-16 mx-auto bg-green-100 dark:bg-green-900 rounded-full flex items-center justify-center">
                <Icon name="Mail" size="lg" class="text-green-600 dark:text-green-400" />
            </div>
            <div>
                <h1 class="text-2xl font-bold text-gray-900 dark:text-white">이메일을 확인해주세요</h1>
                <p class="mt-2 text-sm text-gray-600 dark:text-gray-400">
                    <span class="font-medium text-gray-800 dark:text-gray-200">{email}</span>로<br/>
                    비밀번호 재설정 링크를 발송했습니다.
                </p>
            </div>
            <div class="pt-4 space-y-3">
                <p class="text-xs text-gray-500 dark:text-gray-400">
                    이메일이 도착하지 않았나요? 스팸 폴더를 확인해주세요.
                </p>
                <button
                    onclick={() => isSubmitted = false}
                    class="text-sm text-primary hover:text-primary-600 font-medium"
                >
                    다른 이메일로 다시 시도
                </button>
            </div>
        </div>
    {:else}
        <!-- 이메일 입력 폼 -->
        <div class="text-center lg:text-left">
            <h1 class="text-2xl font-bold text-gray-900 dark:text-white">비밀번호 찾기</h1>
            <p class="mt-2 text-sm text-gray-600 dark:text-gray-400">
                가입하신 이메일을 입력하시면<br class="lg:hidden"/>
                비밀번호 재설정 링크를 보내드립니다.
            </p>
        </div>

        {#if error}
            <div class="p-4 bg-red-50 dark:bg-red-900/20 border border-red-200 dark:border-red-800 rounded-lg flex items-start gap-3">
                <Icon name="AlertCircle" size="sm" class="text-red-500 flex-shrink-0 mt-0.5" />
                <p class="text-sm text-red-600 dark:text-red-400">{error}</p>
            </div>
        {/if}

        <form onsubmit={handleSubmit} class="space-y-4">
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

            <button
                type="submit"
                disabled={isLoading}
                class="w-full py-2.5 bg-primary text-white font-medium rounded-lg hover:bg-primary-600 disabled:opacity-50 disabled:cursor-not-allowed transition-colors flex items-center justify-center gap-2"
            >
                {#if isLoading}
                    <Icon name="Loader2" size="sm" class="animate-spin" />
                    전송 중...
                {:else}
                    재설정 링크 받기
                {/if}
            </button>
        </form>

        <div class="text-center">
            <a href="/auth/login" class="text-sm text-gray-500 dark:text-gray-400 hover:text-primary flex items-center justify-center gap-1">
                <Icon name="ArrowLeft" size="sm" />
                로그인으로 돌아가기
            </a>
        </div>
    {/if}
</div>