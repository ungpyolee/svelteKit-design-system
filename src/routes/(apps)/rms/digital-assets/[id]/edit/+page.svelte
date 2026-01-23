<!-- /rms/digital-assets/[id]/edit/+page.svelte -->
<script>
    import Icon from "$lib/icons/icon.svelte";
    import Modal from "$lib/components/ui/Modal.svelte";
    import { cn } from "$lib/utils";
    import { goto } from '$app/navigation';
    import { page } from '$app/state';

    // 기존 데이터 로드 (실제로는 API에서 가져옴)
    let originalData = {
        id: "a1b2c3d4-5678-9012-abcd-ef1234567890",
        assetNumber: "IPM-2024-0892",
        title: "고효율 영구자석 동기전동기 설계 도면",
        subTitle: "IPM 모터 냉각 구조 및 자석 배치 상세 설계",
        bodyCopy: "본 설계 도면은 고효율 IPMSM 모터의 전체 구조를 포함합니다. 냉각 시스템 설계, V자형 자석 배치, 로터/스테이터 상세 치수가 포함되어 있으며, 전기차 구동 모터 적용을 목표로 설계되었습니다. 최대 효율 97.2%, 정격 출력 150kW 사양입니다.",
        tags: ["IPMSM", "전기차", "냉각설계", "고효율", "150kW"],
        machineType: "mt-001",
        assetType: "at-001",
        disclosureRange: "team",
        isGrantRequiredItem: false,
        listPrice: 50000,
        thumbnailKey: "https://rms-api.motorspace.org/images/thumbnails/088c1cbb-670a-4767-9f02-69209dfc85bc/af55379d-cc8c-46a5-b864-4689b07329f7.png",
        assetFiles: [
            {
                id: "file-001",
                displayName: "IPM_Motor_Assembly_v1.2.dwg",
                description: "전체 조립도",
                fileSize: 25690112,
                fileExtension: ".dwg",
            },
            {
                id: "file-002",
                displayName: "IPM_Cooling_System.pdf",
                description: "냉각 시스템 상세 설명서",
                fileSize: 8540160,
                fileExtension: ".pdf",
            }
        ],
    };

    // 폼 데이터 (기존 데이터로 초기화)
    let formData = $state({
        assetNumber: originalData.assetNumber,
        title: originalData.title,
        subTitle: originalData.subTitle,
        bodyCopy: originalData.bodyCopy,
        tags: [...originalData.tags],
        machineType: originalData.machineType,
        assetType: originalData.assetType,
        disclosureRange: originalData.disclosureRange,
        isGrantRequiredItem: originalData.isGrantRequiredItem,
        listPrice: originalData.listPrice,
    });

    // 태그 입력
    let tagInput = $state('');

    // 파일 (기존 파일 + 새 파일)
    let existingFiles = $state([...originalData.assetFiles]);
    let newFiles = $state([]);
    let deletedFileIds = $state([]);

    // 썸네일
    let thumbnailFile = $state(null);
    let thumbnailPreview = $state(originalData.thumbnailKey);
    let thumbnailChanged = $state(false);

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

    // 태그 관리
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

    // 기존 파일 삭제
    function removeExistingFile(fileId) {
        existingFiles = existingFiles.filter(f => f.id !== fileId);
        deletedFileIds = [...deletedFileIds, fileId];
    }

    // 새 파일 업로드
    function handleFileUpload(e) {
        const files = Array.from(e.target.files);
        const uploaded = files.map(file => ({
            id: crypto.randomUUID(),
            file: file,
            displayName: file.name,
            description: '',
            fileSize: file.size,
            fileExtension: '.' + file.name.split('.').pop(),
            isNew: true,
        }));
        newFiles = [...newFiles, ...uploaded];
    }

    function removeNewFile(fileId) {
        newFiles = newFiles.filter(f => f.id !== fileId);
    }

    function updateFileDescription(fileId, description, isExisting) {
        if (isExisting) {
            existingFiles = existingFiles.map(f => 
                f.id === fileId ? { ...f, description } : f
            );
        } else {
            newFiles = newFiles.map(f => 
                f.id === fileId ? { ...f, description } : f
            );
        }
    }

    // 썸네일 업로드
    function handleThumbnailUpload(e) {
        const file = e.target.files[0];
        if (file) {
            thumbnailFile = file;
            thumbnailChanged = true;
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
        thumbnailChanged = true;
    }

    // 유틸 함수
    function formatFileSize(bytes) {
        if (bytes < 1024) return bytes + ' B';
        if (bytes < 1048576) return (bytes / 1024).toFixed(1) + ' KB';
        return (bytes / 1048576).toFixed(1) + ' MB';
    }

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

    // 변경 여부 확인
    let hasChanges = $derived(() => {
        return formData.title !== originalData.title ||
               formData.subTitle !== originalData.subTitle ||
               formData.bodyCopy !== originalData.bodyCopy ||
               formData.machineType !== originalData.machineType ||
               formData.assetType !== originalData.assetType ||
               formData.disclosureRange !== originalData.disclosureRange ||
               formData.isGrantRequiredItem !== originalData.isGrantRequiredItem ||
               formData.listPrice !== originalData.listPrice ||
               JSON.stringify(formData.tags) !== JSON.stringify(originalData.tags) ||
               deletedFileIds.length > 0 ||
               newFiles.length > 0 ||
               thumbnailChanged;
    });

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
            console.log('Updating:', {
                id: originalData.id,
                ...formData,
                deletedFileIds,
                newFiles,
                thumbnailFile,
                thumbnailChanged,
            });
            
            await new Promise(resolve => setTimeout(resolve, 1000));
            goto(`/rms/digital-assets/${originalData.id}`);
        } catch (error) {
            console.error('Update error:', error);
        } finally {
            isSubmitting = false;
        }
    }

    // 취소 확인 모달
    let showCancelModal = $state(false);

    function handleCancel() {
        if (hasChanges()) {
            showCancelModal = true;
        } else {
            goto(`/rms/digital-assets/${originalData.id}`);
        }
    }

    // 삭제 확인 모달
    let showDeleteModal = $state(false);

    async function handleDelete() {
        try {
            console.log('Deleting asset:', originalData.id);
            await new Promise(resolve => setTimeout(resolve, 1000));
            goto('/rms/digital-assets');
        } catch (error) {
            console.error('Delete error:', error);
        }
    }
</script>

<div class="space-y-6">
    <!-- 브레드크럼 -->
    <nav class="flex items-center gap-2 text-sm text-gray-500 dark:text-gray-400">
        <a href="/rms/digital-assets" class="hover:text-primary">Digital Assets</a>
        <Icon name="ChevronRight" size="xs" />
        <a href="/rms/digital-assets/{originalData.id}" class="hover:text-primary">{originalData.assetNumber}</a>
        <Icon name="ChevronRight" size="xs" />
        <span class="text-gray-800 dark:text-gray-200">수정</span>
    </nav>

    <!-- 헤더 -->
    <div class="flex items-center justify-between">
        <div>
            <h1 class="text-2xl font-bold text-gray-800 dark:text-gray-100">기술자료 수정</h1>
            <p class="text-gray-500 dark:text-gray-400 mt-1">{originalData.assetNumber}</p>
        </div>
        <button 
            onclick={() => showDeleteModal = true}
            class="px-4 py-2 text-sm font-medium text-danger border border-danger rounded-lg hover:bg-danger hover:text-white transition-colors"
        >
            <Icon name="Trash" size="sm" class="inline mr-1" />
            삭제
        </button>
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
                    <!-- 자료 번호 (수정 불가) -->
                    <div>
                        <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
                            자료 번호
                        </label>
                        <input 
                            type="text"
                            value={formData.assetNumber}
                            disabled
                            class="w-full px-4 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-gray-100 dark:bg-gray-800 text-gray-500 dark:text-gray-400 cursor-not-allowed"
                        />
                        <p class="text-xs text-gray-400 mt-1">자료 번호는 수정할 수 없습니다.</p>
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
                    <p class="text-lg font-medium text-gray-800 dark:text-gray-100">
                        첨부파일
                        <span class="text-sm font-normal text-gray-500">({existingFiles.length + newFiles.length}개)</span>
                    </p>
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

                    <!-- 기존 파일 목록 -->
                    {#if existingFiles.length > 0}
                        <div>
                            <p class="text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">기존 파일</p>
                            <div class="space-y-2">
                                {#each existingFiles as file}
                                    <div class="flex items-start gap-3 p-3 bg-gray-50 dark:bg-gray-800 rounded-lg">
                                        <div class="w-10 h-10 rounded-lg bg-primary-100 dark:bg-primary-900/30 flex items-center justify-center text-primary-600 dark:text-primary-400 flex-shrink-0">
                                            <Icon name={getFileIcon(file.fileExtension)} size="md" />
                                        </div>
                                        <div class="flex-1 min-w-0">
                                            <p class="text-sm font-medium text-gray-800 dark:text-gray-200 truncate">{file.displayName}</p>
                                            <p class="text-xs text-gray-500 dark:text-gray-400">{formatFileSize(file.fileSize)}</p>
                                            <input 
                                                type="text"
                                                placeholder="파일 설명"
                                                value={file.description}
                                                oninput={(e) => updateFileDescription(file.id, e.target.value, true)}
                                                class="mt-2 w-full px-3 py-1.5 text-sm border border-gray-200 dark:border-gray-700 rounded bg-white dark:bg-gray-900 text-gray-800 dark:text-gray-200 focus:ring-1 focus:ring-primary outline-none"
                                            />
                                        </div>
                                        <button 
                                            type="button"
                                            onclick={() => removeExistingFile(file.id)}
                                            class="p-1.5 text-gray-400 hover:text-danger hover:bg-gray-200 dark:hover:bg-gray-700 rounded transition-colors"
                                        >
                                            <Icon name="Trash" size="sm" />
                                        </button>
                                    </div>
                                {/each}
                            </div>
                        </div>
                    {/if}

                    <!-- 새 파일 목록 -->
                    {#if newFiles.length > 0}
                        <div>
                            <p class="text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">새로 추가된 파일</p>
                            <div class="space-y-2">
                                {#each newFiles as file}
                                    <div class="flex items-start gap-3 p-3 bg-success-50 dark:bg-success-900/10 border border-success-200 dark:border-success-800 rounded-lg">
                                        <div class="w-10 h-10 rounded-lg bg-success-100 dark:bg-success-900/30 flex items-center justify-center text-success-600 dark:text-success-400 flex-shrink-0">
                                            <Icon name={getFileIcon(file.fileExtension)} size="md" />
                                        </div>
                                        <div class="flex-1 min-w-0">
                                            <div class="flex items-center gap-2">
                                                <p class="text-sm font-medium text-gray-800 dark:text-gray-200 truncate">{file.displayName}</p>
                                                <span class="px-1.5 py-0.5 text-xs bg-success-100 text-success-700 dark:bg-success-900/30 dark:text-success-400 rounded">NEW</span>
                                            </div>
                                            <p class="text-xs text-gray-500 dark:text-gray-400">{formatFileSize(file.fileSize)}</p>
                                            <input 
                                                type="text"
                                                placeholder="파일 설명"
                                                value={file.description}
                                                oninput={(e) => updateFileDescription(file.id, e.target.value, false)}
                                                class="mt-2 w-full px-3 py-1.5 text-sm border border-gray-200 dark:border-gray-700 rounded bg-white dark:bg-gray-900 text-gray-800 dark:text-gray-200 focus:ring-1 focus:ring-primary outline-none"
                                            />
                                        </div>
                                        <button 
                                            type="button"
                                            onclick={() => removeNewFile(file.id)}
                                            class="p-1.5 text-gray-400 hover:text-danger hover:bg-gray-200 dark:hover:bg-gray-700 rounded transition-colors"
                                        >
                                            <Icon name="X" size="sm" />
                                        </button>
                                    </div>
                                {/each}
                            </div>
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
                            {#if thumbnailChanged}
                                <span class="absolute top-2 left-2 px-2 py-1 text-xs bg-success-500 text-white rounded">변경됨</span>
                            {/if}
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
                    disabled={!isValid || isSubmitting || !hasChanges()}
                    class="w-full px-4 py-2.5 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 disabled:opacity-50 disabled:cursor-not-allowed transition-colors"
                >
                    {#if isSubmitting}
                        저장 중...
                    {:else}
                        변경사항 저장
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
<Modal bind:isOpen={showCancelModal} title="수정 취소" size="sm">
    <div class="text-center">
        <div class="w-12 h-12 mx-auto mb-4 rounded-full bg-warning-100 dark:bg-warning-900/30 flex items-center justify-center text-warning-600 dark:text-warning-400">
            <Icon name="AlertTriangle" size="md" />
        </div>
        <p class="text-gray-800 dark:text-gray-200 mb-2">수정을 취소하시겠습니까?</p>
        <p class="text-sm text-gray-500 dark:text-gray-400">
            변경된 내용이 저장되지 않습니다.
        </p>
    </div>

    {#snippet footer()}
        <button 
            onclick={() => showCancelModal = false}
            class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
        >
            계속 수정
        </button>
        <button 
            onclick={() => goto(`/rms/digital-assets/${originalData.id}`)}
            class="px-4 py-2 text-sm font-medium bg-danger text-white rounded-lg hover:bg-danger-600 transition-colors"
        >
            취소하기
        </button>
    {/snippet}
</Modal>

<!-- 삭제 확인 모달 -->
<Modal bind:isOpen={showDeleteModal} title="기술자료 삭제" size="sm">
    <div class="text-center">
        <div class="w-12 h-12 mx-auto mb-4 rounded-full bg-danger-100 dark:bg-danger-900/30 flex items-center justify-center text-danger-600 dark:text-danger-400">
            <Icon name="Trash" size="md" />
        </div>
        <p class="text-gray-800 dark:text-gray-200 mb-2">정말 삭제하시겠습니까?</p>
        <p class="text-sm text-gray-500 dark:text-gray-400">
            이 작업은 되돌릴 수 없습니다.<br/>
            <span class="font-medium text-gray-700 dark:text-gray-300">{originalData.assetNumber}</span> 자료가 영구적으로 삭제됩니다.
        </p>
    </div>

    {#snippet footer()}
        <button 
            onclick={() => showDeleteModal = false}
            class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
        >
            취소
        </button>
        <button 
            onclick={handleDelete}
            class="px-4 py-2 text-sm font-medium bg-danger text-white rounded-lg hover:bg-danger-600 transition-colors"
        >
            삭제하기
        </button>
    {/snippet}
</Modal>