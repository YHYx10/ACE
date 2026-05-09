<template>
    <div class="dynamic">        
        <Equip 
            :drag="darag"
            @onMouseDown="onMouseDown"
            @onDoubleClick="onDoubleClick"
        />
    </div>
</template>

<script>
import Equip from './Equip'

export default {
    props: ['darag'],
    methods: {
        onMouseDown(dragObject){
            this.$emit("onMouseDown", dragObject);
        },
        onDoubleClick(adress, item){
            this.$emit("onDoubleClick", adress, item);
        }
    },
    components:{
        Equip
    }
}
</script>

<style lang="scss" scoped>
.dynamic{
    height: 100%;
    &-tittle{
        font-weight: bold;
        color: #fff;
        font-size: 1.3vh;
        width: 100%;
        margin-bottom: .5vh;
    }
}
</style>