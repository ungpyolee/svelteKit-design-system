<!-- /rms/digital-assets/[id]/edit/+page.svelte -->
<script>
    import Icon from "$lib/icons/icon.svelte";
    import Modal from "$lib/components/ui/Modal.svelte";
    import { goto } from '$app/navigation';
    import { page } from '$app/state';
    import { EditorJs, EditorMinimal } from "$lib/components/editor";
    import { Breadcrumb, PageHeader } from "$lib/components/ui";

    // URL에서 ID 가져오기
    const assetId = page.params.id;

    // ========== 더미 데이터 (기존 자료) ==========
    const originalData = {
        id: assetId,
        assetNumber: "IPM-2024-0892",
        title: "고효율 영구자석 동기전동기 설계 도면",
        summary: "본 설계 도면은 고효율 IPMSM 모터의 전체 구조를 포함합니다. 냉각 시스템 설계, V자형 자석 배치, 로터/스테이터 상세 치수가 포함되어 있습니다.",
        tags: ["IPMSM", "전기차", "냉각설계", "고효율", "150kW"],
        machineType: "mt-001",
        assetType: "at-001",
        detailContent: {
            time: 1706169421000,
            blocks: [
                {
                    type: "header",
                    data: { text: "IPMSM 모터 설계 개요", level: 2 }
                },
                {
                    type: "paragraph",
                    data: { text: "본 설계는 전기차 구동용 150kW급 IPMSM 모터의 상세 설계 도면입니다. 최대 효율 97.2%를 달성하기 위한 최적화된 자석 배치와 냉각 구조를 포함합니다." }
                },
                {
                    type: "header",
                    data: { text: "주요 특징", level: 3 }
                },
                {
                    type: "list",
                    data: {
                        style: "unordered",
                        items: [
                            "V자형 자석 배치로 높은 토크 밀도 실현",
                            "수냉식 냉각 시스템 적용",
                            "분할 코어 구조로 제조 용이성 확보",
                            "NVH 최적화 설계 반영"
                        ]
                    }
                }
            ]
        },
        metaTitle: "고효율 IPMSM 전동기 설계도면 | 전기차 모터 전문",
        metaKeywords: "IPMSM, 전기차 모터, 영구자석 동기전동기, 150kW, 고효율 모터",
        metaDescription: "97.2% 고효율 달성 IPMSM 설계 도면입니다. V자형 자석 배치, 수냉식 냉각 구조 포함. 전기차 구동 모터 개발에 최적화된 설계입니다.",
        disclosureRange: "국내전동기 관련 제조업체/연구기관",
        isGrantRequiredItem: true,
        listPrice: 0,
        thumbnailKey: "https://picsum.photos/seed/motor1/800/450",
        assetFiles: [
            {
                id: "file-001",
                displayName: "IPM_Motor_Assembly_v1.2.dwg",
                description: "전체 조립도 - 3D CAD 파일",
                fileSize: 25690112,
                fileExtension: ".dwg",
            },
            {
                id: "file-002",
                displayName: "IPM_Cooling_System.pdf",
                description: "냉각 시스템 상세 설명서",
                fileSize: 8540160,
                fileExtension: ".pdf",
            },
            {
                id: "file-003",
                displayName: "Rotor_Stator_Specs.xlsx",
                description: "로터/스테이터 상세 사양표",
                fileSize: 1245184,
                fileExtension: ".xlsx",
            }
        ],
    };

    // ========== 단계 관리 ==========
    let currentStep = $state(1);
    const totalSteps = 2;

    function nextStep() {
        currentStep = 2;
    }

    function prevStep() {
        if (currentStep > 1) {
            currentStep = 1;
        }
    }

    // ========== 폼 데이터 (기존 데이터로 초기화) ==========
    let formData = $state({
        title: originalData.title,
        summary: originalData.summary,
        tags: [...originalData.tags],
        machineType: originalData.machineType,
        assetType: originalData.assetType,
        detailContent: originalData.detailContent,
        metaTitle: originalData.metaTitle,
        metaKeywords: originalData.metaKeywords,
        metaDescription: originalData.metaDescription,
        disclosureRange: originalData.disclosureRange,
        isGrantRequiredItem: originalData.isGrantRequiredItem,
        listPrice: originalData.listPrice,
    });

    // 태그 입력
    let tagInput = $state('');

    // 파일 (기존 + 새 파일)
    let existingFiles = $state([...originalData.assetFiles]);
    let newFiles = $state([]);
    let deletedFileIds = $state([]);

    // 썸네일
    let thumbnailPreview = $state(originalData.thumbnailKey);
    let thumbnailFile = $state(null);
    let thumbnailChanged = $state(false);
    let isGenerating = $state(false);
    let activeTab = $state('upload');
    let aiPrompt = $state('');

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

    // ========== 태그 관리 ==========
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

    // ========== 파일 관리 ==========
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

    function removeExistingFile(fileId) {
        existingFiles = existingFiles.filter(f => f.id !== fileId);
        deletedFileIds = [...deletedFileIds, fileId];
    }

    function removeNewFile(fileId) {
        newFiles = newFiles.filter(f => f.id !== fileId);
    }

    function updateExistingFileDescription(fileId, description) {
        existingFiles = existingFiles.map(f =>
            f.id === fileId ? { ...f, description } : f
        );
    }

    function updateNewFileDescription(fileId, description) {
        newFiles = newFiles.map(f =>
            f.id === fileId ? { ...f, description } : f
        );
    }

    // ========== EditorJs ===========
    let editorRef;
    let descriptionData = $state(originalData.detailContent);

    // ========== 썸네일 ==========
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

    // ========== AI 이미지 생성 ==========
    async function generateAIImage() {
        if (!aiPrompt.trim()) return;

        isGenerating = true;
        await new Promise(resolve => setTimeout(resolve, 2500));

        thumbnailPreview = `https://picsum.photos/seed/${Date.now()}/800/450`;
        thumbnailFile = null;
        thumbnailChanged = true;

        isGenerating = false;
    }

    // ========== 유틸리티 ==========
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

    // ========== 유효성 검사 ==========
    let isStep1Valid = $derived(
        formData.title.trim() !== '' &&
        formData.machineType !== '' &&
        formData.assetType !== ''
    );

    let isStep2Valid = $derived(
        formData.metaTitle.trim() !== ''
    );

    // ========== AI 메타 태그 생성 ==========
    let isGeneratingMeta = $state(false);

    async function generateMetaTags() {
        if (!isStep1Valid) {
            alert('메타 태그 생성을 위해 기본 정보(제목, 기계 유형, 자산 유형)를 먼저 입력해주세요.');
            return;
        }

        isGeneratingMeta = true;
        await new Promise(resolve => setTimeout(resolve, 1500));

        const titleBase = formData.title || '제품';
        const machineType = machineTypes.find(m => m.id === formData.machineType)?.displayName || '장비';
        const assetType = assetTypes.find(a => a.id === formData.assetType)?.displayName || '자산';
        const summary = formData.summary || '';

        formData.metaTitle = `${titleBase} | ${machineType} ${assetType} - 최고의 선택`;
        formData.metaKeywords = `${titleBase}, ${machineType}, ${assetType}, 고품질, 전문 장비, 산업용`;
        formData.metaDescription = summary
            ? `${summary.slice(0, 100)}... 지금 바로 확인하세요.`
            : `${titleBase}은(는) 뛰어난 성능과 신뢰성을 갖춘 ${machineType} ${assetType}입니다. 전문가가 추천하는 최고의 제품을 만나보세요.`;

        isGeneratingMeta = false;
    }

    // ========== 변경 여부 확인 ==========
    let hasChanges = $derived(
        formData.title !== originalData.title ||
        formData.summary !== originalData.summary ||
        formData.machineType !== originalData.machineType ||
        formData.assetType !== originalData.assetType ||
        formData.disclosureRange !== originalData.disclosureRange ||
        formData.isGrantRequiredItem !== originalData.isGrantRequiredItem ||
        formData.listPrice !== originalData.listPrice ||
        formData.metaTitle !== originalData.metaTitle ||
        formData.metaKeywords !== originalData.metaKeywords ||
        formData.metaDescription !== originalData.metaDescription ||
        JSON.stringify(formData.tags) !== JSON.stringify(originalData.tags) ||
        deletedFileIds.length > 0 ||
        newFiles.length > 0 ||
        thumbnailChanged
    );

    // ========== 제출 ==========
    let isSubmitting = $state(false);
    let showConfirmModal = $state(false);

    function handleSubmitClick() {
        showConfirmModal = true;
    }

    async function handleConfirmSubmit() {
        isSubmitting = true;
        showConfirmModal = false;

        try {
            console.log('Updating:', {
                id: originalData.id,
                ...formData,
                descriptionData,
                existingFiles,
                newFiles,
                deletedFileIds,
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

    // ========== 취소 ==========
    let showCancelModal = $state(false);

    function handleCancel() {
        if (hasChanges) {
            showCancelModal = true;
        } else {
            goto(`/rms/digital-assets/${originalData.id}`);
        }
    }

    // ========== 삭제 ==========
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
    <Breadcrumb
        items={[
            { label: 'RMS' },
            { label: '기술자료', href: '/rms/digital-assets' },
            { label: originalData.assetNumber, href: `/rms/digital-assets/${originalData.id}` },
            { label: '수정' }
        ]}
    />

    <!-- 헤더 -->
    <div class="flex items-start justify-between">
        <PageHeader
            title="기술자료 수정"
            description="{originalData.assetNumber} - {originalData.title}"
        />
        <button
            onclick={() => showDeleteModal = true}
            class="px-4 py-2 text-sm font-medium text-danger border border-danger rounded-lg hover:bg-danger hover:text-white transition-colors flex items-center gap-2"
        >
            <Icon name="Trash" size="sm" />
            삭제
        </button>
    </div>

    <!-- 스텝 인디케이터 -->
    <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-6">
        <div class="flex items-center justify-center">
            <div class="flex items-center gap-4">
                <!-- Step 1 -->
                <div class="flex items-center gap-2">
                    <div class="w-8 h-8 rounded-full flex items-center justify-center text-sm font-medium {currentStep >= 1 ? 'bg-primary text-white' : 'bg-gray-200 dark:bg-gray-700 text-gray-500'}">
                        {#if currentStep > 1}
                            <Icon name="Check" size="sm" />
                        {:else}
                            1
                        {/if}
                    </div>
                    <span class="text-sm font-medium {currentStep >= 1 ? 'text-gray-800 dark:text-gray-200' : 'text-gray-500'}">
                        기본 정보
                    </span>
                </div>

                <!-- Connector -->
                <div class="w-20 h-0.5 {currentStep > 1 ? 'bg-primary' : 'bg-gray-200 dark:bg-gray-700'}"></div>

                <!-- Step 2 -->
                <div class="flex items-center gap-2">
                    <div class="w-8 h-8 rounded-full flex items-center justify-center text-sm font-medium {currentStep >= 2 ? 'bg-primary text-white' : 'bg-gray-200 dark:bg-gray-700 text-gray-500'}">
                        2
                    </div>
                    <span class="text-sm font-medium {currentStep >= 2 ? 'text-gray-800 dark:text-gray-200' : 'text-gray-500'}">
                        메타 정보 & 공개 설정
                    </span>
                </div>
            </div>
        </div>
    </div>

    <!-- ==================== Step 1: 기본 정보 ==================== -->
    {#if currentStep === 1}
        <div class="grid grid-cols-1 lg:grid-cols-3 gap-6">
            <!-- 왼쪽: 메인 폼 -->
            <div class="lg:col-span-2 space-y-6">
                <!-- 기본 정보 -->
                <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
                    <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800">
                        <p class="text-lg font-medium text-gray-800 dark:text-gray-100">기본 정보</p>
                    </header>
                    <section class="p-6 space-y-4">

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

                        <!-- 요약설명 -->
                        <div>
                            <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
                                요약설명
                            </label>
                            <textarea
                                bind:value={formData.summary}
                                rows="3"
                                placeholder="기술자료에 대한 간략한 요약을 입력하세요"
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
                                                    oninput={(e) => updateExistingFileDescription(file.id, e.target.value)}
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
                                                    placeholder="파일 설명 (선택)"
                                                    value={file.description}
                                                    oninput={(e) => updateNewFileDescription(file.id, e.target.value)}
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

                <!-- 상세설명 -->
                <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
                    <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800">
                        <p class="text-lg font-medium text-gray-800 dark:text-gray-100">상세설명</p>
                    </header>
                    <section class="px-12 py-4">
                        <EditorJs
                            bind:this={editorRef}
                            data={descriptionData}
                            placeholder="기술자료에 대한 상세 설명을 입력하세요..."
                            onChange={(data) => descriptionData = data}
                        />
                    </section>
                </div>
            </div>

            <!-- 오른쪽: 썸네일 & 액션 -->
            <div class="space-y-6">
                <!-- 썸네일 -->
                <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
                    <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800">
                        <p class="text-lg font-medium text-gray-800 dark:text-gray-100">썸네일</p>
                    </header>

                    <section class="p-6">
                        {#if thumbnailPreview}
                            <!-- 썸네일 미리보기 -->
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
                            <!-- 탭 선택 -->
                            <div class="flex gap-1 p-1 bg-gray-100 dark:bg-gray-800 rounded-lg mb-4">
                                <button
                                    type="button"
                                    onclick={() => activeTab = 'upload'}
                                    class="flex-1 flex items-center justify-center gap-2 px-4 py-2 text-sm font-medium rounded-md transition-colors {activeTab === 'upload' ? 'bg-white dark:bg-gray-700 text-gray-900 dark:text-white shadow-sm' : 'text-gray-500 dark:text-gray-400 hover:text-gray-700 dark:hover:text-gray-300'}"
                                >
                                    <Icon name="Upload" size="sm" />
                                    직접 업로드
                                </button>
                                <button
                                    type="button"
                                    onclick={() => activeTab = 'ai'}
                                    class="flex-1 flex items-center justify-center gap-2 px-4 py-2 text-sm font-medium rounded-md transition-colors {activeTab === 'ai' ? 'bg-white dark:bg-gray-700 text-gray-900 dark:text-white shadow-sm' : 'text-gray-500 dark:text-gray-400 hover:text-gray-700 dark:hover:text-gray-300'}"
                                >
                                    <Icon name="Sparkles" size="sm" />
                                    AI 생성
                                </button>
                            </div>

                            <!-- 직접 업로드 -->
                            {#if activeTab === 'upload'}
                                <label class="block">
                                    <div class="border-2 border-dashed border-gray-200 dark:border-gray-700 rounded-lg p-6 text-center hover:border-primary dark:hover:border-primary transition-colors cursor-pointer aspect-video flex flex-col items-center justify-center">
                                        <Icon name="Image" size="lg" class="text-gray-400 mb-2" />
                                        <p class="text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">클릭하여 이미지 업로드</p>
                                        <p class="text-xs text-gray-500 dark:text-gray-400">PNG, JPG, GIF (최대 10MB)</p>
                                    </div>
                                    <input
                                        type="file"
                                        accept="image/*"
                                        onchange={handleThumbnailUpload}
                                        class="hidden"
                                    />
                                </label>

                            <!-- AI 생성 -->
                            {:else}
                                <div class="space-y-4">
                                    <div class="border-2 border-dashed border-gray-200 dark:border-gray-700 rounded-lg p-6 aspect-video flex flex-col items-center justify-center">
                                        {#if isGenerating}
                                            <!-- 로딩 상태 -->
                                            <div class="flex flex-col items-center gap-3">
                                                <div class="relative">
                                                    <div class="w-12 h-12 border-4 border-primary/30 rounded-full"></div>
                                                    <div class="absolute inset-0 w-12 h-12 border-4 border-primary border-t-transparent rounded-full animate-spin"></div>
                                                </div>
                                                <div class="text-center">
                                                    <p class="text-sm font-medium text-gray-700 dark:text-gray-300">AI가 이미지를 생성하고 있어요</p>
                                                    <p class="text-xs text-gray-500 dark:text-gray-400 mt-1">잠시만 기다려주세요...</p>
                                                </div>
                                            </div>
                                        {:else}
                                            <Icon name="Sparkles" size="lg" class="text-primary mb-2" />
                                            <p class="text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">AI로 썸네일 생성</p>
                                            <p class="text-xs text-gray-500 dark:text-gray-400">원하는 이미지를 설명해주세요</p>
                                        {/if}
                                    </div>

                                    <!-- 프롬프트 입력 -->
                                    <div class="space-y-3">
                                        <textarea
                                            bind:value={aiPrompt}
                                            placeholder="예: 파란 하늘 아래 펼쳐진 초록 들판, 미니멀한 스타일"
                                            disabled={isGenerating}
                                            class="w-full px-4 py-3 text-sm border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-900 dark:text-white placeholder-gray-400 focus:outline-none focus:ring-2 focus:ring-primary/50 focus:border-primary disabled:opacity-50 disabled:cursor-not-allowed resize-none"
                                            rows="2"
                                        ></textarea>

                                        <button
                                            type="button"
                                            onclick={generateAIImage}
                                            disabled={isGenerating || !aiPrompt.trim()}
                                            class="w-full flex items-center justify-center gap-2 px-4 py-2.5 bg-primary text-white text-sm font-medium rounded-lg hover:bg-primary/90 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
                                        >
                                            {#if isGenerating}
                                                <div class="w-4 h-4 border-2 border-white/30 border-t-white rounded-full animate-spin"></div>
                                                생성 중...
                                            {:else}
                                                <Icon name="Sparkles" size="sm" />
                                                이미지 생성하기
                                            {/if}
                                        </button>
                                    </div>
                                </div>
                            {/if}
                        {/if}
                    </section>
                </div>

                <!-- 액션 버튼 -->
                <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-6 space-y-3">
                    <button
                        onclick={nextStep}
                        class="w-full px-4 py-2.5 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 disabled:opacity-50 disabled:cursor-not-allowed transition-colors flex items-center justify-center gap-2"
                    >
                        다음 단계
                        <Icon name="ArrowRight" size="sm" />
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
    {/if}

    <!-- ==================== Step 2: 메타 정보 & 공개 설정 ==================== -->
    {#if currentStep === 2}
        <div class="grid grid-cols-1 lg:grid-cols-3 gap-6">
            <!-- 메타 태그 정보 -->
            <div class="lg:col-span-2 space-y-6">
                <!-- 메타 태그 정보 -->
                <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl">
                    <header class="px-6 py-4 border-b border-gray-100 dark:border-gray-800 flex items-start justify-between gap-4">
                        <div>
                            <p class="text-lg font-medium text-gray-800 dark:text-gray-100">메타 태그 정보</p>
                            <p class="text-sm text-gray-500 dark:text-gray-400 mt-1">검색 엔진 최적화(SEO)를 위한 메타 정보를 입력하세요.</p>
                        </div>
                        <button
                            type="button"
                            onclick={generateMetaTags}
                            disabled={isGeneratingMeta || !isStep1Valid}
                            class="flex items-center gap-2 px-4 py-2 bg-gradient-to-r from-violet-500 to-purple-600 text-white text-sm font-medium rounded-lg hover:from-violet-600 hover:to-purple-700 transition-all disabled:opacity-50 disabled:cursor-not-allowed shrink-0"
                        >
                            {#if isGeneratingMeta}
                                <div class="w-4 h-4 border-2 border-white border-t-transparent rounded-full animate-spin"></div>
                                생성 중...
                            {:else}
                                <Icon name="Sparkles" size="sm" />
                                AI 자동 생성
                            {/if}
                        </button>
                    </header>

                    <section class="p-6 space-y-4">
                        <!-- 유효성 안내 메시지 -->
                        {#if !isStep1Valid}
                            <div class="flex items-center gap-2 p-3 bg-amber-50 dark:bg-amber-900/20 border border-amber-200 dark:border-amber-800 rounded-lg">
                                <Icon name="AlertCircle" size="sm" class="text-amber-500 shrink-0" />
                                <p class="text-sm text-amber-700 dark:text-amber-400">
                                    AI 자동 생성을 사용하려면 기본 정보(제목, 기계 유형, 자산 유형)를 먼저 입력해주세요.
                                </p>
                            </div>
                        {/if}

                        <!-- HTML 메타 타이틀 -->
                        <div>
                            <div class="flex items-center justify-between mb-1">
                                <label class="block text-sm font-medium text-gray-700 dark:text-gray-300">
                                    HTML 메타 타이틀 <span class="text-danger">*</span>
                                </label>
                                <span class="text-xs text-gray-400">
                                    {formData.metaTitle.length}/60
                                </span>
                            </div>
                            <input
                                type="text"
                                bind:value={formData.metaTitle}
                                placeholder="검색 결과에 표시될 제목"
                                class="w-full px-4 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none transition-all"
                            />
                            <p class="text-xs text-gray-500 mt-1">권장: 50-60자</p>
                        </div>

                        <!-- HTML 메타 키워드 -->
                        <div>
                            <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-1">
                                HTML 메타 키워드
                            </label>
                            <input
                                type="text"
                                bind:value={formData.metaKeywords}
                                placeholder="키워드1, 키워드2, 키워드3"
                                class="w-full px-4 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none transition-all"
                            />
                            <p class="text-xs text-gray-500 mt-1">쉼표로 구분하여 입력</p>
                        </div>

                        <!-- HTML 메타 설명 -->
                        <div>
                            <div class="flex items-center justify-between mb-1">
                                <label class="block text-sm font-medium text-gray-700 dark:text-gray-300">
                                    HTML 메타 설명
                                </label>
                                <span class="text-xs text-gray-400">
                                    {formData.metaDescription.length}/160
                                </span>
                            </div>
                            <textarea
                                bind:value={formData.metaDescription}
                                rows="3"
                                placeholder="검색 결과에 표시될 설명"
                                class="w-full px-4 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none transition-all resize-none"
                            ></textarea>
                            <p class="text-xs text-gray-500 mt-1">권장: 150-160자</p>
                        </div>

                        <!-- 검색 결과 미리보기 -->
                        {#if formData.metaTitle || formData.metaDescription}
                            <div class="mt-6 pt-6 border-t border-gray-100 dark:border-gray-800">
                                <p class="text-sm font-medium text-gray-700 dark:text-gray-300 mb-3">검색 결과 미리보기</p>
                                <div class="p-4 bg-gray-50 dark:bg-gray-800 rounded-lg">
                                    <p class="text-blue-600 dark:text-blue-400 text-lg font-medium truncate">
                                        {formData.metaTitle || '메타 타이틀을 입력하세요'}
                                    </p>
                                    <p class="text-green-700 dark:text-green-500 text-sm mt-1">
                                        https://example.com/digital-assets/{originalData.id}
                                    </p>
                                    <p class="text-gray-600 dark:text-gray-400 text-sm mt-1 line-clamp-2">
                                        {formData.metaDescription || '메타 설명을 입력하세요'}
                                    </p>
                                </div>
                            </div>
                        {/if}
                    </section>
                </div>
            </div>

            <div class="space-y-6">
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
                                placeholder="예: 국내전동기 관련 제조업체/연구기관"
                                class="w-full px-4 py-2 border border-gray-200 dark:border-gray-700 rounded-lg bg-white dark:bg-gray-800 text-gray-800 dark:text-gray-200 focus:ring-2 focus:ring-primary focus:border-transparent outline-none transition-all"
                            />
                        </div>

                        <!-- 승인 필요 여부 -->
                        <div>
                            <label class="flex items-center gap-3 p-3 border border-gray-200 dark:border-gray-700 rounded-lg cursor-pointer hover:bg-gray-50 dark:hover:bg-gray-800 transition-colors">
                                <input
                                    type="checkbox"
                                    bind:checked={formData.isGrantRequiredItem}
                                    class="w-4 h-4 rounded border-gray-300 text-primary focus:ring-primary"
                                />
                                <div>
                                    <p class="text-sm font-medium text-gray-800 dark:text-gray-200">승인 필요</p>
                                    <p class="text-xs text-gray-500 dark:text-gray-400">이 기술자료 (DB)는 무료로 제공하되 저작권자의 허가가 필요한 자료입니다.</p>
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
                <div class="bg-white dark:bg-gray-900 border border-gray-100 dark:border-gray-800 rounded-xl p-6">
                    <div class="flex items-center gap-3">
                        <button
                            type="button"
                            onclick={prevStep}
                            class="flex-1 px-4 py-2.5 text-sm font-medium text-gray-700 dark:text-gray-300 border border-gray-200 dark:border-gray-700 rounded-lg hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors flex items-center justify-center gap-2"
                        >
                            <Icon name="ArrowLeft" size="sm" />
                            이전 단계
                        </button>
                        <button
                            onclick={handleSubmitClick}
                            disabled={isSubmitting}
                            class="flex-1 px-4 py-2.5 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 disabled:opacity-50 disabled:cursor-not-allowed transition-colors"
                        >
                            {#if isSubmitting}
                                저장 중...
                            {:else}
                                변경사항 저장
                            {/if}
                        </button>
                    </div>
                </div>
            </div>
        </div>
    {/if}
</div>


<!-- 저장 확인 모달 -->
<Modal bind:isOpen={showConfirmModal} title="변경사항 저장" size="sm">
    <div class="text-center">
        <div class="w-12 h-12 mx-auto mb-4 rounded-full bg-primary-100 dark:bg-primary-900/30 flex items-center justify-center text-primary-600 dark:text-primary-400">
            <Icon name="Save" size="md" />
        </div>
        <p class="text-gray-800 dark:text-gray-200 mb-2">변경사항을 저장하시겠습니까?</p>
        <p class="text-sm text-gray-500 dark:text-gray-400">
            수정된 내용이 즉시 반영됩니다.
        </p>
    </div>

    {#snippet footer()}
        <button
            onclick={() => showConfirmModal = false}
            class="px-4 py-2 text-sm font-medium text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-800 rounded-lg transition-colors"
        >
            취소
        </button>
        <button
            onclick={handleConfirmSubmit}
            disabled={isSubmitting}
            class="px-4 py-2 text-sm font-medium bg-primary text-white rounded-lg hover:bg-primary-600 disabled:opacity-50 disabled:cursor-not-allowed transition-colors"
        >
            {#if isSubmitting}
                저장 중...
            {:else}
                저장하기
            {/if}
        </button>
    {/snippet}
</Modal>


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
