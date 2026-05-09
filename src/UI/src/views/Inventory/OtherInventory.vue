<template>
    <div class="other" :style="{'top': `${y}px`, 'left': `${x}px`}">
        <div class="other-tittle" @mousedown="onMoveStart">
            <!-- <img src="/img/inventory/common/backpack.png" alt="backpack" class="other-tittle-img">
            <div class="other-tittle-weight">
                <div class="other-tittle-weight-current">{{(currentWeight/1000).toFixed()}}</div>
                <div class="other-tittle-weight-max">/{{(maxWeight/1000).toFixed()}} kg</div>
            </div>
            <div class="other-tittle-name">{{ loc(name) }}</div> -->
        </div>
        <div class="space" @mousedown="onMoveStart">
      <div class="header">
        <div class="left">
          <img src="/img/inventory/common/bagpack.svg" alt="bagpack" />
          <div class="title">{{ loc(name) }}</div>
        </div>
        <div class="weight">
          <WeightSVG />
          <div class="weight-cur">
            <knob-control
              style="font-size: 2rem !important;"
              readOnly="true"
              :size="50"
              primaryColor="#5CFF80"
              secondaryColor="rgba(255, 255, 255, 0.25)"
              textColor="white"
              :min="0"
              :max="Math.ceil(maxWeight / 1000) || 1"
              :stroke-width="9"
              :value="Math.ceil(currentWeight / 1000)"
            ></knob-control>
          </div>
        </div>
      </div>
    </div>
    <div class="list">
        <ListItems
            :id="id"
            :drag="drag"
            :sortByIndex="true"
            :canDrop="true"
            :width="'100%'"
            :type="'bagpack'"
            @onMouseDown="onMouseDown"
            @onDoubleClick="onDoubleClick"
            class="other-inventory"
        />
    </div>

    
    <!-- <div class="other-footer" @click="close">
            {{loc('inv_close_other')}}
        </div> -->
    <CrossSVG @click.prevent="close" class="cross-svg" />
  </div>
</template>

<script>
import ListItems from './ListItems'
import {mapGetters} from 'vuex'
import CrossSVG from './componentsSVG/CrossSVG.vue'
import WeightSVG from './componentsSVG/WeightSVG.vue'
import KnobControl from 'vue-knob-control'

export default {
    props:['id', 'name', 'drag'],
    data() {
        return {
            x: 150,
            y: 300,
            offsetX: 0,
            offsetY: 0,
            move: false,
            maxWeight: 1
        }
    },
    computed: {
        ...mapGetters('inventory', ['getById']),
        ...mapGetters('localization', ['loc']),
        currentWeight(){
            const inventory = this.getById(this.id);
            let total = 0;
            inventory.items.forEach(item => {
                total += item.getWeight();
            });
            return total;
        }
    },
    methods: {
        onMouseDown(dragObject){
            this.$emit("onMouseDown", dragObject)
        },
        onDoubleClick(adress, item){
            this.$emit("onDoubleClick", adress, item)
        },
        onMouseRelease(e){
            if(e.button == 0) this.move = false;
        },
        onMousemove(e){
            if(this.move){
                this.x = e.clientX + this.offsetX;
                this.y = e.clientY + this.offsetY;
            }
        },
        onMoveStart(e){
            if(e.button == 0) {
                const pos = e.target.getBoundingClientRect(); 
                this.offsetX = pos.x - e.clientX; 
                this.offsetY = pos.y - e.clientY; 
                this.move = true;
            }
        },
        close(){
            this.$store.commit('inventory/closeOther', this.id)
        }
    },
    components:{
        ListItems,
        CrossSVG,
        WeightSVG,
        KnobControl,
    },
    mounted(){
        this.maxWeight = this.getById(this.id).maxWeight;
        document.addEventListener("mouseup", this.onMouseRelease)
        document.addEventListener("mousemove", this.onMousemove)
    },
    beforeDestroy(){
        document.removeEventListener("mouseup", this.onMouseRelease)
        document.removeEventListener("mousemove", this.onMousemove)
    }
}
</script>

<style lang="scss" scoped>
.other {
  position: fixed;
  background: radial-gradient(50% 50% at 50% 50%, rgba(28, 29, 34, 0.94) 0%, rgba(17, 17, 20, 0.97) 100%);
    border: 1px solid rgba(255, 255, 255, 0.1);
    border-radius: 2px;
  box-shadow: 0 1.5rem 3rem rgba(0, 13, 18, 0.7);
  // width: 23.4vh;
  width: 420px;
  height: 556px;
  //overflow: hidden;
  .cross-svg {
    position: absolute;
    top: 16px;
    right: 47px;
    opacity: 0.55;
    transition: 0.3s ease;
    &:hover {
      opacity: 1;
    }
    // border: 1px solid red;
  }

  .space {
    display: flex;
    align-items: flex-end;
    height: 134px;
    width: 100%;
  }
  .header {
    display: flex;
    width: 100%;
    align-items: center;
    justify-content: space-between;
    margin: 0 35px;
    padding-bottom: 20px;
    .left {
      display: flex;
      gap: 15px;
      .title {
        font-weight: 700;
        font-size: 20px;
        line-height: 24px;
        text-transform: uppercase;
        color: #301934 ;
        font-family: 'Akrobat';
      }
    }
    .weight {
      display: flex;
      gap: 17px;
      align-items: center;
    }
  }

  .list {
    overflow-y: auto;
    height: 380px;
    margin: 0 25px 35px;
    padding-right: 10px;
    &::-webkit-scrollbar {
        width: 5px;
    }
    &::-webkit-scrollbar-track {
        background: rgba(0, 0, 0, 0.25);
    }
    &::-webkit-scrollbar-thumb {
        background: #301934 ;
    }
  }
}
</style>