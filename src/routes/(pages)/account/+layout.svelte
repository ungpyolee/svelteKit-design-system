<!-- /routes/(pages)/account/+layout.svelte -->
<script>
    import Icon from "$lib/icons/icon.svelte";
    import { page } from '$app/stores';

    let { children } = $props();

    const menuItems = [
        { href: '/account', label: '내 프로필', icon: 'User' },
        { href: '/account/settings', label: '계정 설정', icon: 'Settings' },
        { href: '/account/security', label: '보안', icon: 'Shield' },
        { href: '/account/notifications', label: '알림 설정', icon: 'Bell' },
    ];

    function isActive(href) {
        if (href === '/account') {
            return $page.url.pathname === '/account';
        }
        return $page.url.pathname.startsWith(href);
    }
</script>

<div class="min-h-screen bg-gray-50 dark:bg-gray-950">
    <!-- 헤더 -->
    <header class="bg-white dark:bg-gray-900 border-b border-gray-200 dark:border-gray-800">
        <div class="max-w-6xl mx-auto px-4 sm:px-6 lg:px-8">
            <div class="flex items-center justify-between h-16">
                <div class="flex items-center gap-4">
                    <a href="/rms/user-dashboard" class="flex items-center gap-2 text-primary">
                        <div class="w-8 h-8 bg-primary-100 dark:bg-primary-900 rounded-lg flex items-center justify-center">
                            <Icon name="Zap" size="sm" />
                        </div>
                        <span class="text-lg font-bold">RMS</span>
                    </a>
                    <span class="text-gray-300 dark:text-gray-700">/</span>
                    <span class="text-sm font-medium text-gray-600 dark:text-gray-400">계정 관리</span>
                </div>
                <a 
                    href="/rms/user-dashboard" 
                    class="text-sm text-gray-500 dark:text-gray-400 hover:text-primary flex items-center gap-1"
                >
                    <Icon name="ArrowLeft" size="sm" />
                    대시보드로 돌아가기
                </a>
            </div>
        </div>
    </header>

    <div class="max-w-6xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        <div class="flex flex-col lg:flex-row gap-8">
            <!-- 사이드 메뉴 -->
            <aside class="lg:w-64 flex-shrink-0">
                <nav class="bg-white dark:bg-gray-900 border border-gray-200 dark:border-gray-800 rounded-xl p-2 space-y-1">
                    {#each menuItems as item}
                        <a
                            href={item.href}
                            class="flex items-center gap-3 px-4 py-2.5 rounded-lg text-sm font-medium transition-colors {isActive(item.href) ? 'bg-primary-50 dark:bg-primary-900/30 text-primary' : 'text-gray-600 dark:text-gray-400 hover:bg-gray-50 dark:hover:bg-gray-800'}"
                        >
                            <Icon name={item.icon} size="sm" />
                            {item.label}
                        </a>
                    {/each}
                    
                    <div class="pt-2 mt-2 border-t border-gray-100 dark:border-gray-800">
                        <a
                            href="/account/delete"
                            class="flex items-center gap-3 px-4 py-2.5 rounded-lg text-sm font-medium text-red-600 dark:text-red-400 hover:bg-red-50 dark:hover:bg-red-900/20 transition-colors"
                        >
                            <Icon name="UserX" size="sm" />
                            회원 탈퇴
                        </a>
                    </div>
                </nav>
            </aside>

            <!-- 메인 콘텐츠 -->
            <main class="flex-1 min-w-0">
                {@render children()}
            </main>
        </div>
    </div>
</div>