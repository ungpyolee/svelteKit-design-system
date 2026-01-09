<script>
  import * as icons from 'lucide-svelte';

  /**
   * 아이콘 이름 (Lucide 아이콘명)
   * @type {string}
   */
  export let name;

  /**
   * 아이콘 크기
   * @type {'sm' | 'md' | 'lg' | 'xl'}
   */
  export let size = 'md';

  /**
   * 아이콘 색상
   * @type {string}
   */
  export let color = 'currentColor';

  /**
   * 추가 클래스
   * @type {string}
   */
  let className = '';
  export { className as class };

  const sizes = {
    sm: 16,
    md: 20,
    lg: 24,
    xl: 32,
  };

  $: IconComponent = icons[name];
  $: pixelSize = sizes[size] || sizes.md;
</script>

{#if IconComponent}
  <svelte:component
    this={IconComponent}
    size={pixelSize}
    {color}
    class={className}
    {...$$restProps}
  />
{:else}
  <span class="text-destructive text-sm">Icon '{name}' not found</span>
{/if}