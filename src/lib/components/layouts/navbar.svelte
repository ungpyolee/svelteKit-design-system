<script>
    import { sidebar, theme } from '$lib/stores';
    import Icon from '$lib/icons/icon.svelte';
    import { ThemeToggle } from "$lib/components/ui";
    import { cn } from '$lib/utils';
    import { clickOutside } from '$lib/actions/clickOutside.js';
    
    let isUserMenuOpen = $state(false);
</script>
<div
    class={cn("fixed top-0 left-0 right-0 p-3 bg-white dark:bg-gray-900" 
            ,"border-b border-gray-100 dark:border-gray-800",
            $sidebar.expanded? "left-[280px]": "left-[84px]")}
>
    <!-- <div class="absolute top-full left-0 right-0 bg-gray-100 h-[calc(100vh-64px)]">
    </div>
     -->
    <nav class="flex">
        
        <div class="">
            <button
                onclick={() => sidebar.toggle()}
                class="p-2 rounded-md hover:bg-gray-50 dark:hover:bg-gray-800"
                aria-label="사이드바 토글"
                >
                {#if $sidebar.expanded}
                    <Icon name="ListIndentDecrease" size="lg" />
                {:else}
                    <Icon name="ListIndentIncrease" size="lg" />
                {/if}
            </button>
        </div>
        <div class="ms-auto flex">
            <ThemeToggle/>

            <!-- user button -->
            <div class="relative" use:clickOutside={() => isUserMenuOpen = false}>
                <button onclick={() => isUserMenuOpen = !isUserMenuOpen} type="button"class="rounded-md hover:bg-gray-50 dark:hover:bg-gray-800 transition flex items-center px-2 gap-1">
                    <div class="text-sm text-end ps-2">
                        <p class="font-light text-gray-600 dark:text-gray-400 -mb-1">User name</p>
                        <p class="text-gray-800 dark:text-gray-100">Team name</p>
                    </div>
                    <div class="p-1.5">
                        <Icon name="UserRound" size="lg"/>
                    </div>
                </button>
                
                {#if isUserMenuOpen}
                    <div class={cn("overflow-hidden absolute right-0 top-full bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-lg shadow-lg",
                    )}>
                        <!-- 메뉴 내용 -->
                        <ul class="grid grid-cols-1 p-2 text-sm w-[160px]">
                            <li>
                                <a href="/setting/switch-team" class="block py-2 px-4 hover:text-primary hover:bg-gray-50 dark:hover:bg-gray-800 rounded w-full text-start">
                                    팀 전환
                                </a>
                            </li>
                            <li>
                                <a href="/" class="block py-2 px-4 hover:text-primary hover:bg-gray-50 dark:hover:bg-gray-800 rounded w-full text-start">
                                    계정 정보
                                </a>
                            </li>
                        </ul>
                        <ul class='grid grid-cols-1 p-2 text-sm w-[160px] border-t border-gray-100 dark:border-gray-800'>
                            <li>
                                <button class="block py-2 px-4 hover:text-primary hover:bg-gray-50 dark:hover:bg-gray-800 rounded w-full text-start">
                                    로그아웃
                                </button>
                            </li>
                        </ul>
                    </div>
                {/if}
            </div>
        </div>
    </nav>
    
</div>
