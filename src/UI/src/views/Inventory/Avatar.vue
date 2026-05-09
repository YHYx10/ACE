<template>
    <div 
        class="avatar" 
        v-if="drag !== null && drag.dragStart" 
        :style="{'left': `${drag.clientX - 10}px`, 'top': `${drag.clientY}px`}"
    >
        <img :src="drag.item.getImage()" alt="avatar">
        <!-- <div class="avatar-split" v-if="drag.alt">
            Split
        </div> -->
         <!-- <div class="avatar-drop" v-if="!drag.alt && drag.overAdress == 'out' ">
            <div>Drop</div>
            <img src="/img/inventory/common/drop_2.svg" alt="drop">
        </div> -->
    </div>
</template>

<script>
export default {
    props:['drag']
}
</script>

<style lang="scss" scoped>
    .avatar{
        position: fixed;
        width: 4vh;
        height: 4vh;
        pointer-events: none;
        &-split{
            color: #fff;
            position: absolute;
            top: -.7vh;
            left: 0;
            font-size: .7vh;
            line-height: .85vh;
            letter-spacing: .05vh;
        }
        &-drop{
            display: flex;
            align-items: center;
            justify-content: space-between;
            color: rgba(#fff, .5);
            position: absolute;
            top: -.7vh;
            font-size: .7vh;
            line-height: .85vh;
            letter-spacing: .05vh;
            img{
                width: .7vh;
                height: .7vh;
                margin-left: .3vh;
            }
        }
    }
</style>