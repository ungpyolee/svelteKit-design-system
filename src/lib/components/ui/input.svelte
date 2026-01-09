<script>
  import { cn } from '$lib/utils/cn.js';
  import Icon from '$lib/icons/icon.svelte';

  /**
   * 입력값
   * @type {string}
   */
  export let value = '';

  /**
   * placeholder
   * @type {string}
   */
  export let placeholder = '';

  /**
   * 라벨
   * @type {string}
   */
  export let label = '';

  /**
   * 에러 메시지
   * @type {string}
   */
  export let error = '';

  /**
   * 힌트 메시지
   * @type {string}
   */
  export let hint = '';

  /**
   * 아이콘 이름 (선택)
   * @type {string | undefined}
   */
  export let icon = undefined;

  /**
   * 입력 타입
   * @type {'text' | 'email' | 'password' | 'number' | 'tel' | 'url' | 'search'}
   */
  export let type = 'text';

  /**
   * 비활성화 상태
   * @type {boolean}
   */
  export let disabled = false;

  /**
   * 필수 여부
   * @type {boolean}
   */
  export let required = false;

  /**
   * 고유 ID
   * @type {string}
   */
  export let id = `input-${Math.random().toString(36).slice(2, 9)}`;

  /**
   * 추가 클래스
   * @type {string}
   */
  let className = '';
  export { className as class };

  const inputStyles = cn(
    'flex h-10 w-full rounded-md border bg-background px-3 py-2 text-sm transition-colors',
    'file:border-0 file:bg-transparent file:text-sm file:font-medium',
    'placeholder:text-muted-foreground',
    'focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2',
    'disabled:cursor-not-allowed disabled:opacity-50',
    error ? 'border-destructive focus-visible:ring-destructive' : 'border-input',
    icon ? 'pl-10' : ''
  );
</script>

<div class={cn('w-full', className)}>
  {#if label}
    <label for={id} class="block text-sm font-medium mb-1.5">
      {label}
      {#if required}
        <span class="text-destructive">*</span>
      {/if}
    </label>
  {/if}

  <div class="relative">
    {#if icon}
      <div class="absolute left-3 top-1/2 -translate-y-1/2 text-muted-foreground">
        <Icon name={icon} size="sm" />
      </div>
    {/if}

    <input
      {id}
      {type}
      {placeholder}
      {disabled}
      {required}
      bind:value
      class={inputStyles}
      {...$$restProps}
    />
  </div>

  {#if error}
    <p class="mt-1.5 text-sm text-destructive">{error}</p>
  {:else if hint}
    <p class="mt-1.5 text-sm text-muted-foreground">{hint}</p>
  {/if}
</div>