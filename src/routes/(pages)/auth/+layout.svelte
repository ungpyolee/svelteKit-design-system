<!-- /routes/(pages)/auth/+layout.svelte -->
<script>
    import Icon from "$lib/icons/icon.svelte";
    
    let { children } = $props();
</script>

<div class="min-h-screen bg-gray-50 dark:bg-gray-950 flex">
    <!-- 좌측: 브랜딩 영역 (데스크톱만) -->
    <div class="hidden lg:flex lg:w-1/2 bg-gradient-to-br from-primary-600 to-primary-800 p-12 flex-col justify-between relative overflow-hidden">
        <!-- 배경 패턴 -->
        <div class="absolute inset-0 opacity-10">
            <svg class="w-full h-full" viewBox="0 0 100 100" preserveAspectRatio="none">
                <defs>
                    <pattern id="grid" width="10" height="10" patternUnits="userSpaceOnUse">
                        <path d="M 10 0 L 0 0 0 10" fill="none" stroke="white" stroke-width="0.5"/>
                    </pattern>
                </defs>
                <rect width="100" height="100" fill="url(#grid)" />
            </svg>
        </div>
        
        <!-- 로고 -->
        <div class="relative z-10">
            <a href="/" class="flex items-center gap-3 text-white">
                <div class="w-10 h-10 bg-white/20 rounded-xl flex items-center justify-center">
                    <Icon name="Zap" size="md" />
                </div>
                <span class="text-2xl font-bold">RMS</span>
            </a>
        </div>
        
        <!-- 메인 텍스트 -->
        <div class="relative z-10">
            <h1 class="text-4xl font-bold text-white mb-4">
                전동기 기술자료<br/>마켓플레이스
            </h1>
            <p class="text-lg text-white/80 leading-relaxed">
                고품질 전동기 설계 도면, 사양서, 시험성적서를<br/>
                한 곳에서 검색하고 거래하세요.
            </p>
        </div>
        
        <!-- 하단 통계 -->
        <div class="relative z-10 flex gap-8">
            <div>
                <p class="text-3xl font-bold text-white">1,200+</p>
                <p class="text-sm text-white/70">기술자료</p>
            </div>
            <div>
                <p class="text-3xl font-bold text-white">350+</p>
                <p class="text-sm text-white/70">기여자</p>
            </div>
            <div>
                <p class="text-3xl font-bold text-white">50+</p>
                <p class="text-sm text-white/70">기업 고객</p>
            </div>
        </div>
    </div>
    
    <!-- 우측: 폼 영역 -->
    <div class="flex-1 flex items-center justify-center p-6 lg:p-12">
        <div class="w-full max-w-md">
            <!-- 모바일 로고 -->
            <div class="lg:hidden mb-8 text-center">
                <a href="/" class="inline-flex items-center gap-2 text-primary">
                    <div class="w-10 h-10 bg-primary-100 dark:bg-primary-900 rounded-xl flex items-center justify-center">
                        <Icon name="Zap" size="md" />
                    </div>
                    <span class="text-2xl font-bold">RMS</span>
                </a>
            </div>
            
            {@render children()}
        </div>
    </div>
</div>