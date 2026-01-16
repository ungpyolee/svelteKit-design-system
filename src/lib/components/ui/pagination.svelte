<!-- $lib/components/ui/Pagination.svelte -->
<script>
    import Icon from "$lib/icons/icon.svelte";
    import { cn } from "$lib/utils";
    
    let { 
        currentPage = $bindable(1), 
        totalPages = 1,
        totalItems = 0,
        itemsPerPage = 10,
        showInfo = true,
        size = "md", // sm, md, lg
        onPageChange = () => {}
    } = $props();
    
    // 사이즈별 스타일
    const sizeClasses = {
        sm: 'h-8 w-8 text-sm',
        md: 'h-10 w-10 text-sm',
        lg: 'h-12 w-12 text-base'
    };
    
    // 표시할 페이지 번호 계산
    let visiblePages = $derived(() => {
        const delta = 2;
        const range = [];
        const rangeWithDots = [];
        let l;

        for (let i = 1; i <= totalPages; i++) {
            if (i === 1 || i === totalPages || (i >= currentPage - delta && i <= currentPage + delta)) {
                range.push(i);
            }
        }

        for (let i of range) {
            if (l) {
                if (i - l === 2) {
                    rangeWithDots.push(l + 1);
                } else if (i - l !== 1) {
                    rangeWithDots.push('...');
                }
            }
            rangeWithDots.push(i);
            l = i;
        }

        return rangeWithDots;
    });
    
    // 현재 표시 범위 계산
    let itemRange = $derived(() => {
        const start = (currentPage - 1) * itemsPerPage + 1;
        const end = Math.min(currentPage * itemsPerPage, totalItems);
        return { start, end };
    });
    
    function goToPage(page) {
        if (page >= 1 && page <= totalPages && page !== currentPage) {
            currentPage = page;
            onPageChange(page);
        }
    }
    
    function prevPage() {
        goToPage(currentPage - 1);
    }
    
    function nextPage() {
        goToPage(currentPage + 1);
    }
</script>

<div class="flex flex-col sm:flex-row items-center justify-between gap-4">
    <!-- 정보 표시 -->
    {#if showInfo && totalItems > 0}
        <p class="text-sm text-gray-500 dark:text-gray-400">
            <span class="font-medium text-gray-700 dark:text-gray-300">{itemRange().start}-{itemRange().end}</span>
            of
            <span class="font-medium text-gray-700 dark:text-gray-300">{totalItems}</span>
            items
        </p>
    {/if}
    
    <!-- 페이지네이션 -->
    <nav class="flex items-center gap-1">
        <!-- 이전 버튼 -->
        <button
            onclick={prevPage}
            disabled={currentPage === 1}
            class={cn(
                "flex items-center justify-center rounded-lg border border-gray-200 dark:border-gray-700 transition-colors",
                sizeClasses[size],
                currentPage === 1 
                    ? "text-gray-300 dark:text-gray-600 cursor-not-allowed" 
                    : "text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800"
            )}
        >
            <Icon name="ChevronLeft" size={size === 'sm' ? 'xs' : 'sm'} />
        </button>
        
        <!-- 페이지 번호 -->
        {#each visiblePages() as page}
            {#if page === '...'}
                <span class={cn("flex items-center justify-center text-gray-400 dark:text-gray-500", sizeClasses[size])}>
                    ...
                </span>
            {:else}
                <button
                    onclick={() => goToPage(page)}
                    class={cn(
                        "flex items-center justify-center rounded-lg transition-colors font-medium",
                        sizeClasses[size],
                        currentPage === page 
                            ? "bg-primary text-white" 
                            : "text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800 border border-gray-200 dark:border-gray-700"
                    )}
                >
                    {page}
                </button>
            {/if}
        {/each}
        
        <!-- 다음 버튼 -->
        <button
            onclick={nextPage}
            disabled={currentPage === totalPages}
            class={cn(
                "flex items-center justify-center rounded-lg border border-gray-200 dark:border-gray-700 transition-colors",
                sizeClasses[size],
                currentPage === totalPages 
                    ? "text-gray-300 dark:text-gray-600 cursor-not-allowed" 
                    : "text-gray-600 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-800"
            )}
        >
            <Icon name="ChevronRight" size={size === 'sm' ? 'xs' : 'sm'} />
        </button>
    </nav>
</div>