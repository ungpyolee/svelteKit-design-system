<!-- /rms/digital-assets/new/+page.svelte -->
<script>
    import Icon from "$lib/icons/icon.svelte";
    import Modal from "$lib/components/ui/Modal.svelte";
    import { cn } from "$lib/utils";
    import { goto } from '$app/navigation';

    // 폼 데이터
    let formData = $state({
        assetNumber: '',
        title: '',
        subTitle: '',
        bodyCopy: '',
        tags: [],
        machineType: '',
        assetType: '',
        disclosureRange: 'team',
        isGrantRequiredItem: true,
        listPrice: 0,
    });

    // 태그 입력
    let tagInput = $state('');

    // 파일 업로드
    let uploadedFiles = $state([]);
    let thumbnailFile = $state(null);
    let thumbnailPreview = $state('');

    // 옵션 데이터
    const machineTypes = [
        { id: 'mt-001', displayName: 'IPMSM', description: 'Interior Permanent Magnet Synchronous Motor' },
        { id: 'mt-002', displayName: 'SPMSM', description: 'Surface Permanent Magnet Synchronous Motor' },
        { id: 'mt-003', displayName: 'IM', description: 'Induction Motor' },
        { id: 'mt-004', displayName: 'BLDC', description: 'Brushless DC Motor' },
        { id: 'mt-005', displayName: 'SRM', description: 'Switched Reluctance Motor' },
    ];

    const assetTypes = [
        { id: 'at-001', displayName: '설계도면', description: 'CAD 설계 도면 및 3D 모델' },
        { id: 'at-002', displayName: '사양서', description: '기술 사양 문서' },
        { id: 'at-003', displayName: '회로도', description: '전자 회로 설계 문서' },
        { id: 'at-004', displayName: '기술문서', description: '기술 설명 및 API 문서' },
        { id: 'at-005', displayName: '해석보고서', description: '시뮬레이션 및 해석 결과' },
    ];

    // 태그 추가
    function addTag() {
        const tag = tagInput.trim();
        if (tag && !formData.tags.includes(tag)) {
            formData.tags = [...formData.tags, tag];
            tagInput = '';
        }
    }

    function removeTag(tagToRemove) {
        formData.tags = formData.tags.filter(tag => tag !== tagToRemove);
    }

    function handleTagKeydown(e) {
        if (e.key === 'Enter') {
            e.preventDefault();
            addTag();
        }
    }

    // 파일 업로드
    function handleFileUpload(e) {
        const files = Array.from(e.target.files);
        const newFiles = files.map(file => ({
            id: crypto.randomUUID(),
            file: file,
            displayName: file.name,
            description: '',
            fileSize: file.size,
            fileExtension: '.' + file.name.split('.').pop(),
        }));
        uploadedFiles = [...uploadedFiles, ...newFiles];
    }

    function removeFile(fileId) {
        uploadedFiles = uploadedFiles.filter(f => f.id !== fileId);
    }

    function updateFileDescription(fileId, description) {
        uploadedFiles = uploadedFiles.map(f => 
            f.id === fileId ? { ...f, description } : f
        );
    }

    // 썸네일 업로드
    function handleThumbnailUpload(e) {
        const file = e.target.files[0];
        if (file) {
            thumbnailFile = file;
            const reader = new FileReader();
            reader.onload = (e) => {
                thumbnailPreview = e.target.result;
            };
            reader.readAsDataURL(file);
        }
    }

    function removeThumbnail() {
        thumbnailFile = null;
        thumbnailPreview = '';
    }

    // 파일 크기 포맷
    function formatFileSize(bytes) {
        if (bytes < 1024) return bytes + ' B';
        if (bytes < 1048576) return (bytes / 1024).toFixed(1) + ' KB';
        return (bytes / 1048576).toFixed(1) + ' MB';
    }

    // 파일 아이콘
    function getFileIcon(extension) {
        const icons = {
            '.pdf': 'FileText',
            '.dwg': 'FileBox',
            '.doc': 'FileText',
            '.docx': 'FileText',
            '.xls': 'FileSpreadsheet',
            '.xlsx': 'FileSpreadsheet',
            '.zip': 'FileArchive',
            '.sch': 'FileCode',
        };
        return icons[extension] || 'File';
    }

    // 폼 유효성 검사
    let isValid = $derived(
        formData.assetNumber.trim() !== '' &&
        formData.title.trim() !== '' &&
        formData.machineType !== '' &&
        formData.assetType !== ''
    );

    // 제출
    let isSubmitting = $state(false);

    async function handleSubmit() {
        if (!isValid) return;
        
        isSubmitting = true;
        
        try {
            // API 호출 로직
            console.log('Submitting:', {
                ...formData,
                files: uploadedFiles,
                thumbnail: thumbnailFile
            });
            
            // 성공 시 목록으로 이동
            await new Promise(resolve => setTimeout(resolve, 1000)); // 임시 딜레이
            goto('/rms/digital-assets');
        } catch (error) {
            console.error('Submit error:', error);
        } finally {
            isSubmitting = false;
        }
    }

    // 취소 확인 모달
    let showCancelModal = $state(false);

    function handleCancel() {
        const hasData = formData.title || formData.bodyCopy || uploadedFiles.length > 0;
        if (hasData) {
            showCancelModal = true;
        } else {
            goto('/rms/digital-assets');
        }
    }
</script>

<div class="space-y-6">
    <!-- 브레드크럼 -->
    <nav class="flex items-center gap-2 text-sm text-gray-500 dark:text-gray-400">
        <a href="/rms/digital-assets" class="hover:text-primary">Digital Assets</a>
        <Icon name="ChevronRight" size="xs" />
        <span class="text-gray-800 dark:text-gray-200">새 기술자료 등록</span>
    </nav>

    <!-- 헤더 -->
    <div class="flex items-center justify-between">
        <div>
            <h1 class="text-2xl font-bold text-gray-800 dark:text-gray-100">새 기술자료 등록</h1>
            <p class="text-gray-500 dark:text-gray-400 mt-1">새로운 기술자료를 등록합니다.</p>
        </div>
    </div>

    <div class="grid grid-cols-1 lg:grid-cols-3 gap-6">
        <!-- 왼쪽: 메인 폼 -->
        <div class="lg:col-span-2 space-y-6">
            <!-- 기본 정보 -->
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
                <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800">
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">기본 정보</p>
                </header>
                <section class="p-6 space-y-4">
                    <!-- 자료 번호 -->
                    <div>
                        <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
                            자료 번호 <span class="text-danger">*</span>
                        </label>
                        <input 
                            type="text"
                            bind:value={formData.assetNumber}
                            placeholder="예: IPM-2024-0001"
                            class="w-full px-4 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none transition-all"
                        />
                    </div>

                    <!-- 제목 -->
                    <div>
                        <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
                            제목 <span class="text-danger">*</span>
                        </label>
                        <input 
                            type="text"
                            bind:value={formData.title}
                            placeholder="기술자료 제목을 입력하세요"
                            class="w-full px-4 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none transition-all"
                        />
                    </div>

                    <!-- 부제목 -->
                    <div>
                        <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
                            부제목
                        </label>
                        <input 
                            type="text"
                            bind:value={formData.subTitle}
                            placeholder="부제목을 입력하세요 (선택)"
                            class="w-full px-4 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none transition-all"
                        />
                    </div>

                    <!-- 설명 -->
                    <div>
                        <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
                            설명
                        </label>
                        <textarea 
                            bind:value={formData.bodyCopy}
                            rows="5"
                            placeholder="기술자료에 대한 상세 설명을 입력하세요"
                            class="w-full px-4 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none transition-all resize-none"
                        ></textarea>
                    </div>

                    <!-- 태그 -->
                    <div>
                        <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
                            태그
                        </label>
                        <div class="flex gap-2 mb-2">
                            <input 
                                type="text"
                                bind:value={tagInput}
                                onkeydown={handleTagKeydown}
                                placeholder="태그 입력 후 Enter"
                                class="flex-1 px-4 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none transition-all"
                            />
                            <button 
                                type="button"
                                onclick={addTag}
                                class="px-4 py-2 bg-gray-100 dark:bg-gray-800 text-gray-700 dark:text-gray-300 rounded-lg hover:bg-gray-200 dark:hover:bg-gray-700 transition-colors"
                            >
                                추가
                            </button>
                        </div>
                        {#if formData.tags.length > 0}
                            <div class="flex flex-wrap gap-2">
                                {#each formData.tags as tag}
                                    <span class="inline-flex items-center gap-1 px-3 py-1 bg-primary-100 dark:bg-primary-900/30 text-primary-700 dark:text-primary-400 rounded-full text-sm">
                                        #{tag}
                                        <button 
                                            type="button"
                                            onclick={() => removeTag(tag)}
                                            class="hover:text-primary-900 dark:hover:text-primary-200"
                                        >
                                            <Icon name="X" size="xs" />
                                        </button>
                                    </span>
                                {/each}
                            </div>
                        {/if}
                    </div>
                </section>
            </div>

            <!-- 분류 정보 -->
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
                <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800">
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">분류 정보</p>
                </header>
                <section class="p-6 space-y-4">
                    <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
                        <!-- 전동기 타입 -->
                        <div>
                            <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
                                전동기 타입 <span class="text-danger">*</span>
                            </label>
                            <select 
                                bind:value={formData.machineType}
                                class="w-full px-4 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none transition-all"
                            >
                                <option value="">선택하세요</option>
                                {#each machineTypes as type}
                                    <option value={type.id}>{type.displayName}</option>
                                {/each}
                            </select>
                        </div>

                        <!-- 자료 타입 -->
                        <div>
                            <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
                                자료 타입 <span class="text-danger">*</span>
                            </label>
                            <select 
                                bind:value={formData.assetType}
                                class="w-full px-4 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none transition-all"
                            >
                                <option value="">선택하세요</option>
                                {#each assetTypes as type}
                                    <option value={type.id}>{type.displayName}</option>
                                {/each}
                            </select>
                        </div>
                    </div>
                </section>
            </div>

            <!-- 첨부파일 -->
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
                <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800">
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">첨부파일</p>
                </header>
                <section class="p-6 space-y-4">
                    <!-- 파일 업로드 영역 -->
                    <label class="block">
                        <div class="border-2 border-dashed border-gray-200 dark:border-gray-700 rounded-lg p-8 text-center hover:border-primary dark:hover:border-primary transition-colors cursor-pointer">
                            <div class="w-12 h-12 mx-auto mb-4 rounded-full bg-gray-100 dark:bg-gray-800 flex items-center justify-center text-gray-400">
                                <Icon name="Upload" size="lg" />
                            </div>
                            <p class="text-gray-600 dark:text-gray-400 mb-1">파일을 업로드</p>
                            <p class="text-sm text-gray-400 dark:text-gray-500">PDF, DWG, DOC, XLS 등 (최대 100MB)</p>
                        </div>
                        <input 
                            type="file" 
                            multiple
                            onchange={handleFileUpload}
                            class="hidden"
                        />
                    </label>

                    <!-- 업로드된 파일 목록 -->
                    {#if uploadedFiles.length > 0}
                        <div class="space-y-2">
                            {#each uploadedFiles as file}
                                <div class="flex items-start gap-3 p-3 bg-gray-50 dark:bg-gray-800 rounded-lg">
                                    <div class="w-10 h-10 rounded-lg bg-primary-100 dark:bg-primary-900/30 flex items-center justify-center text-primary-600 dark:text-primary-400 flex-shrink-0">
                                        <Icon name={getFileIcon(file.fileExtension)} size="md" />
                                    </div>
                                    <div class="flex-1 min-w-0">
                                        <p class="text-sm font-medium text-gray-800 dark:text-gray-200 truncate">{file.displayName}</p>
                                        <p class="text-xs text-gray-500 dark:text-gray-400">{formatFileSize(file.fileSize)}</p>
                                        <input 
                                            type="text"
                                            placeholder="파일 설명 (선택)"
                                            value={file.description}
                                            oninput={(e) => updateFileDescription(file.id, e.target.value)}
                                            class="mt-2 w-full px-3 py-1.5 text-sm border border-gray-200 dark:border-gray-700 rounded bg-white dark:bg-gray-900 text-gray-800 dark:text-gray-200 focus:ring-1 focus:ring-primary outline-none"
                                        />
                                    </div>
                                    <button 
                                        type="button"
                                        onclick={() => removeFile(file.id)}
                                        class="p-1.5 text-gray-400 hover:text-danger hover:bg-gray-200 dark:hover:bg-gray-700 rounded transition-colors"
                                    >
                                        <Icon name="Trash" size="sm" />
                                    </button>
                                </div>
                            {/each}
                        </div>
                    {/if}
                </section>
            </div>
        </div>

        <!-- 오른쪽: 사이드 패널 -->
        <div class="space-y-6">
            <!-- 썸네일 -->
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
                <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800">
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">썸네일</p>
                </header>
                <section class="p-6">
                    {#if thumbnailPreview}
                        <div class="relative">
                            <img 
                                src={thumbnailPreview} 
                                alt="Thumbnail preview"
                                class="w-full aspect-video object-cover rounded-lg"
                            />
                            <button 
                                type="button"
                                onclick={removeThumbnail}
                                class="absolute top-2 right-2 p-1.5 bg-black/50 text-white rounded-lg hover:bg-black/70 transition-colors"
                            >
                                <Icon name="X" size="sm" />
                            </button>
                        </div>
                    {:else}
                        <label class="block">
                            <div class="border-2 border-dashed border-gray-200 dark:border-gray-700 rounded-lg p-6 text-center hover:border-primary dark:hover:border-primary transition-colors cursor-pointer aspect-video flex flex-col items-center justify-center">
                                <Icon name="Image" size="lg" class="text-gray-400 mb-2" />
                                <p class="text-sm text-gray-500 dark:text-gray-400">썸네일 이미지 업로드</p>
                            </div>
                            <input 
                                type="file" 
                                accept="image/*"
                                onchange={handleThumbnailUpload}
                                class="hidden"
                            />
                        </label>
                    {/if}
                </section>
            </div>

            <!-- 공개 설정 -->
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
                <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800">
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">공개 설정</p>
                </header>
                <section class="p-6 space-y-4">
                    <!-- 공개 범위 -->
                    <div>
                        <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
                            공개 범위
                        </label>
                        <input 
                            type="text"
                            bind:value={formData.disclosureRange}
                            placeholder="예: 전체 공개, 부서 내 공개"
                            class="w-full px-4 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none transition-all"
                        />
                    </div>

                    <!-- 승인 필요 여부 -->
                    <div>
                        <label class="flex items-center gap-3 p-3 border border-gray-200 dark:border-gray-700 rounded-lg cursor-pointer hover:bg-gray-50 dark:hover:bg-gray-800 transition-colors">
                            <input 
                                type="checkbox"
                                bind:checked={formData.isGrantRequiredItem}
                                onchange={() => {
                                    if (formData.isGrantRequiredItem) {
                                        formData.listPrice = 0;
                                    }
                                }}
                                class="w-4 h-4 rounded border-gray-300 text-primary focus:ring-primary"
                            />
                            <div>
                                <p class="text-sm font-medium text-gray-800 dark:text-gray-200">승인 필요</p>
                                <p class="text-xs text-gray-500 dark:text-gray-400">열람 시 관리자 승인이 필요합니다</p>
                            </div>
                        </label>
                    </div>

                    <!-- 가격 -->
                    <div>
                        <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
                            가격 (원)
                        </label>
                        <input 
                            type="number"
                            bind:value={formData.listPrice}
                            min="0"
                            step="1000"
                            placeholder="0"
                            disabled={formData.isGrantRequiredItem}
                            class="w-full px-4 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none transition-all disabled:bg-gray-100 disabled:dark:bg-gray-700 disabled:cursor-not-allowed disabled:opacity-60"
                        />
                        {#if formData.isGrantRequiredItem}
                            <p class="text-xs text-gray-500 dark:text-gray-400 mt-1">승인 필요 항목은 무료입니다</p>
                        {/if}
                    </div>
                </section>
            </div>

            <!-- 액션 버튼 -->
            <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-6 space-y-3">
                <button 
                    onclick={handleSubmit}
                    disabled={!isValid || isSubmitting}
                    class="w-full px-4 py-2.5 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 disabled:opacity-50 disabled:cursor-not-allowed transition-colors"
                >
                    {#if isSubmitting}
                        등록 중...
                    {:else}
                        기술자료 등록
                    {/if}
                </button>
                <button 
                    type="button"
                    onclick={handleCancel}
                    class="w-full px-4 py-2.5 text-sm font-medium text-gray-700 dark:text-gray-300 border border-gray-200 dark:border-gray-700 rounded-lg hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
                >
                    취소
                </button>
            </div>
        </div>
    </div>
</div>

<!-- 취소 확인 모달 -->
<Modal bind:isOpen={showCancelModal} title="작성 취소" size="sm">
    <div class="text-center">
        <div class="w-12 h-12 mx-auto mb-4 rounded-full bg-warning-100 dark:bg-warning-900/30 flex items-center justify-center text-warning-600 dark:text-warning-400">
            <Icon name="AlertTriangle" size="md" />
        </div>
        <p class="text-gray-800 dark:text-gray-200 mb-2">작성을 취소하시겠습니까?</p>
        <p class="text-sm text-gray-500 dark:text-gray-400">
            작성 중인 내용이 저장되지 않습니다.
        </p>
    </div>

    {#snippet footer()}
        <button 
            onclick={() => showCancelModal = false}
            class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
        >
            계속 작성
        </button>
        <button 
            onclick={() => goto('/rms/digital-assets')}
            class="px-4 py-2 text-sm font-medium bg-danger text-white rounded-lg hover:bg-danger-600 transition-colors"
        >
            취소하기
        </button>
    {/snippet}
</Modal>