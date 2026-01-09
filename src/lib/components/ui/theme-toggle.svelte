<script>
  import { theme } from '$lib/stores/theme.js';
  import { cn } from '$lib/utils/cn.js';
  import Icon from '$lib/icons/icon.svelte';

  /**
   * 추가 클래스
   * @type {string}
   */
  let className = '';
  export { className as class };
</script>

<button
  onclick={() => theme.toggle()}
  class={cn(
    'inline-flex items-center justify-center rounded-md p-2 transition-colors',
    'hover:bg-gray-50 dark:hover:bg-gray-800 hover:text-accent-foreground',
    'focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring',
    className
  )}
  aria-label="테마 변경"
  {...$$restProps}
>
  {#if $theme === 'dark'}
    <Icon name="Sun" size="lg" />
  {:else}
    <Icon name="Moon" size="lg" />
  {/if}
</button>