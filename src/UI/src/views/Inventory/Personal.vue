<template>
    <div class="personal">
        <div class="personal__header">
            <div class="header">
                <svg width="18" height="21" viewBox="0 0 18 21" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path fill-rule="evenodd" clip-rule="evenodd"
                        d="M3.36928 1.1299V2.25975L3.19814 2.29381C2.93853 2.34548 2.31442 2.62587 1.9674 2.84666C1.54571 3.11503 0.901637 3.75764 0.640818 4.17025C0.377892 4.5863 0.13087 5.26697 0.055463 5.78344C0.017812 6.04111 -0.000565973 8.29844 1.32732e-05 12.5862C0.00101379 19.4637 -0.00198778 19.3809 0.26336 19.8678C0.411331 20.1394 0.78742 20.5138 1.09695 20.6977C1.63349 21.0165 1.19684 20.9999 9.00377 20.9999C15.3659 20.9999 16.1229 20.9917 16.3796 20.9205C16.781 20.8092 17.0569 20.6462 17.3728 20.3334C17.6923 20.0172 17.8743 19.6708 17.9551 19.2252C17.996 18.9991 18.0078 16.9612 17.9951 12.2992C17.9747 4.78826 18.0153 5.44024 17.5062 4.45414C17.2689 3.99461 17.1512 3.8341 16.7861 3.47222C16.546 3.23427 16.1788 2.9349 15.97 2.80693C15.5929 2.57578 14.8791 2.26759 14.7209 2.26759C14.6482 2.26759 14.6383 2.13109 14.6383 1.13379V0H12.9532H11.2681V1.0352V2.07041H9.00377H6.73944V1.0352V0H5.05436H3.36928V1.1299ZM15.7441 12.595V14.69H14.6119H13.4798V13.6548V12.6196H7.8716H2.26345V11.5598V10.4999H9.00377H15.7441V12.595Z"
                        fill="white" />
                </svg>

                <div class="header__content">
                    <h2 class="header__title">Inventory</h2>
                    <p class="header__subtitle"> Things you wear <br />
                      With you in a backpack</p>
                </div>
            </div>

            <div class="weight">
                <WeightSVG />

                <div class="weight-cur">
                    <knob-control style="font-size: 2rem !important;"  readOnly="true" :size="50" primaryColor="#5CFF80" secondaryColor="rgba(255, 255, 255, 0.25)" textColor="white" :min="0" :max="Math.ceil(inventory.maxWeight/1000)" :stroke-width="9" :value="Math.ceil(currentWeight / 1000)"></knob-control>
                </div>
            </div>
        </div>
        <ListItems :id="personalId" :drag="drag" :width="6" :sortByIndex="true" :canDrop="true"
            @onMouseDown="onMouseDown" @onDoubleClick="onDoubleClick" />
    </div>
</template>

<script>
import ListItems from './ListItems'
import KnobControl from 'vue-knob-control';
import { mapState, mapGetters } from 'vuex'
import WeightSVG from './componentsSVG/WeightSVG.vue';

export default {
    props: ['drag'],
    data() {
        return {
            num: 20
        }
    },
    methods: {
        onMouseDown(dragObject) {
            this.$emit("onMouseDown", dragObject)
        },
        onDoubleClick(adress, itemId) {
            this.$emit("onDoubleClick", adress, itemId)
        }
    },
    computed: {
        ...mapState('inventory', ['otherInventories', 'personalId']),
        ...mapGetters('inventory', ['getById']),
        ...mapGetters('localization', ['loc']),
        inventory() {
            return this.getById(this.personalId);
        },
        currentWeight() {
            let amount = 0;
            this.inventory.items.forEach(item => {
                amount += item.getWeight();
            });
            return amount;
        },
        progress() {
            return this.currentWeight / this.inventory.maxWeight * 100;
        }
    },
    components: {
        ListItems, KnobControl, WeightSVG
    }
}
</script>

<style lang="scss" scoped>
    .weight-cur {
        margin-bottom: 1.5vh;
    }

    .knob-control__text-display {
        font-size: 2rem !important;
    }

    .personal {
    width: 62vh;

    &__header {
        display: flex;
        justify-content: space-between;

        .header {
            display: flex;
            gap: 1vh;
            margin-bottom: 1.2vh;

            &__title {
                margin-bottom: 1vh;
                font-weight: 700;
                font-size: 2.6vh;
                text-transform: uppercase;

                color: #301934 ;
            }

            &__subtitle {
                font-weight: 300;
                font-size: 1.6vh;
                line-height: 120%;
                text-transform: uppercase;

                color: rgba(255, 255, 255, 0.25);
            }
        }

        .weight {
            display: flex;
            align-items: center;

            svg {
                margin-right: .75vh;
            }

            &-text {
                color: rgba(#fff, .5);
                font-size: .9vh;
                line-height: 1.2vh;
                letter-spacing: .03vh;
            }

            &-amount {
                color: #fff;
                font-size: 2.5vh;
                line-height: 2.5vh;
                letter-spacing: .03vh;
            }

            &-max {
                color: rgba(#fff, .5);
                font-size: .9vh;
                line-height: 1.2vh;
                letter-spacing: .03vh;
                align-self: flex-end;
                margin-bottom: .2vh;
                padding-left: .15vh;
                text-align: left;
            }
        }
    }


}
</style>