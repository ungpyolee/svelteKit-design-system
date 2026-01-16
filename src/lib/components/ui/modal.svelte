<!-- $lib/components/ui/Modal.svelte -->
<script>
    import Icon from "$lib/icons/icon.svelte";
    
    let { 
        isOpen = $bindable(false), 
        title = "", 
        size = "md",  // sm, md, lg, xl
        children,
        footer
    } = $props();
    
    // 모달 열릴 때 스크롤 막기
    $effect(() => {
        if (isOpen) {
            document.body.style.overflow = 'hidden';
        } else {
            document.body.style.overflow = '';
        }
        
        // cleanup
        return () => {
            document.body.style.overflow = '';
        };
    });

    function close() {
        isOpen = false;
    }
    
    function handleBackdropClick(e) {
        if (e.target === e.currentTarget) {
            close();
        }
    }
    
    function handleKeydown(e) {
        if (e.key === 'Escape') {
            close();
        }
    }
    
    const sizeClasses = {
        sm: 'max-w-sm',
        md: 'max-w-md',
        lg: 'max-w-lg',
        xl: 'max-w-xl',
        '2xl': 'max-w-2xl'
    };
</script>

<svelte:window onkeydown={handleKeydown} />

{#if isOpen}
    <!-- Backdrop -->
    <div 
        class="fixed inset-0 z-50 flex items-center justify-center p-4 bg-black/50 backdrop-blur-sm transition-opacity duration-200"
        onclick={handleBackdropClick}
    >
        <!-- Modal -->
        <div class="w-full {sizeClasses[size]} bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl shadow-xl transform transition-all duration-200">
            <!-- Header -->
            <header class="flex items-center justify-between px-6 py-4 border-b border-gray-100 dark:border-gray-800">
                <p class="text-lg font-medium text-gray-800 dark:text-gray-100">{title}</p>
                <button 
                    onclick={close}
                    class="p-1.5 text-gray-400 hover:text-gray-600 dark:hover:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
                >
                    <Icon name="X" size="sm" />
                </button>
            </header>
            
            <!-- Body -->
            <section class="p-6">
                {@render children()}
            </section>
            
            <!-- Footer -->
            {#if footer}
                <footer class="flex items-center justify-end gap-3 px-6 py-4 border-t border-gray-100 dark:border-gray-800">
                    {@render footer()}
                </footer>
            {/if}
        </div>
    </div>
{/if}