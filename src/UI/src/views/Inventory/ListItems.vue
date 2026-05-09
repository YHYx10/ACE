<template>
    <div id="list">
        <div class="list" v-if="!type || type === 'bagpack'" :style="{'width': width == undefined ? '96%' : `auto`}" :class="{bagStyle: type === 'bagpack'}">
            <div class="list-item" :id="canDrop ? 'drop' : 'nodrop'" v-for="(item, index) in items" :key="index"
                :adress="adress(index)"
                :class="[{
                    'list-active': isActive(index),
                }, getHighlightClassByType(item)]"
                @mousedown="onMouseDown(index, item, $event)" @mousemove="onMouseMoveX(index, item, $event)">
                <img v-if="item" :src="item.getImage()" alt="item">
                <div class="list-item-weight" v-if="item && item.count > 1">{{item.count}} </div>
                <!-- <div class="list-item-promo" v-if="item && item.promo"></div> -->
            </div>

            <!-- <div class="blocked-slots" v-if="!backpack && dynamicType === 'Equip'">
                <div class="list-item slot-block opacity-60" v-for="index in 30" :key="index">
                    <div class="block__img">
                        <svg width="1.6vh" viewBox="0 0 12 15" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path fill-rule="evenodd" clip-rule="evenodd"
                                d="M5.17321 0.00991264C4.57007 0.0874455 3.99253 0.3053 3.48935 0.645125C3.2096 0.834076 2.69791 1.35072 2.51077 1.63318C2.24399 2.03588 2.06228 2.45892 1.94421 2.95226C1.89035 3.17724 1.8833 3.31873 1.87157 4.41058L1.85859 5.61974H1.5478C1.36243 5.61974 1.16818 5.63786 1.06639 5.66462C0.565646 5.79632 0.179955 6.18701 0.0488174 6.69532C-0.0162725 6.94759 -0.0162725 13.672 0.0488174 13.9243C0.179955 14.4326 0.565646 14.8233 1.06639 14.955C1.21594 14.9944 1.7739 14.9999 5.57966 14.9999C10.3588 14.9999 10.0774 15.0104 10.4318 14.8196C10.7652 14.6402 11.0524 14.2422 11.1242 13.8599C11.1433 13.7582 11.1538 12.486 11.1538 10.2827C11.1538 6.56148 11.1601 6.71259 10.991 6.37564C10.8873 6.16898 10.6352 5.90947 10.4318 5.80002C10.1857 5.66758 9.96806 5.61974 9.61152 5.61974H9.30073L9.28775 4.41058C9.27602 3.31873 9.26897 3.17724 9.21511 2.95226C9.03508 2.20015 8.72209 1.63198 8.19629 1.10276C7.65711 0.560088 7.09357 0.244534 6.36352 0.0764824C6.12117 0.0206998 5.40219 -0.0195176 5.17321 0.00991264ZM6.0739 1.29367C6.34721 1.35218 6.69806 1.49626 6.91241 1.63801C7.47664 2.01114 7.89706 2.64034 8.01591 3.28948C8.03487 3.39298 8.04739 3.89066 8.04739 4.54056V5.61974H5.57966H3.11193L3.11243 4.52783C3.11298 3.33544 3.12276 3.23754 3.28279 2.81965C3.57389 2.05948 4.25858 1.47609 5.0716 1.29546C5.30284 1.24411 5.83781 1.24314 6.0739 1.29367Z" />
                            <path fill-rule="evenodd" clip-rule="evenodd"
                                d="M5.17321 0.00991264C4.57007 0.0874455 3.99253 0.3053 3.48935 0.645125C3.2096 0.834076 2.69791 1.35072 2.51077 1.63318C2.24399 2.03588 2.06228 2.45892 1.94421 2.95226C1.89035 3.17724 1.8833 3.31873 1.87157 4.41058L1.85859 5.61974H1.5478C1.36243 5.61974 1.16818 5.63786 1.06639 5.66462C0.565646 5.79632 0.179955 6.18701 0.0488174 6.69532C-0.0162725 6.94759 -0.0162725 13.672 0.0488174 13.9243C0.179955 14.4326 0.565646 14.8233 1.06639 14.955C1.21594 14.9944 1.7739 14.9999 5.57966 14.9999C10.3588 14.9999 10.0774 15.0104 10.4318 14.8196C10.7652 14.6402 11.0524 14.2422 11.1242 13.8599C11.1433 13.7582 11.1538 12.486 11.1538 10.2827C11.1538 6.56148 11.1601 6.71259 10.991 6.37564C10.8873 6.16898 10.6352 5.90947 10.4318 5.80002C10.1857 5.66758 9.96806 5.61974 9.61152 5.61974H9.30073L9.28775 4.41058C9.27602 3.31873 9.26897 3.17724 9.21511 2.95226C9.03508 2.20015 8.72209 1.63198 8.19629 1.10276C7.65711 0.560088 7.09357 0.244534 6.36352 0.0764824C6.12117 0.0206998 5.40219 -0.0195176 5.17321 0.00991264ZM6.0739 1.29367C6.34721 1.35218 6.69806 1.49626 6.91241 1.63801C7.47664 2.01114 7.89706 2.64034 8.01591 3.28948C8.03487 3.39298 8.04739 3.89066 8.04739 4.54056V5.61974H5.57966H3.11193L3.11243 4.52783C3.11298 3.33544 3.12276 3.23754 3.28279 2.81965C3.57389 2.05948 4.25858 1.47609 5.0716 1.29546C5.30284 1.24411 5.83781 1.24314 6.0739 1.29367Z" />
                        </svg>
                    </div>
                </div>

                <div class="slot-block-header">
                    <svg width="4vh" viewBox="0 0 29 39" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <path fill-rule="evenodd" clip-rule="evenodd"
                            d="M13.4503 0.0257729C11.8822 0.227358 10.3806 0.793779 9.07231 1.67732C8.34495 2.1686 7.01456 3.51187 6.52799 4.24626C5.83437 5.29329 5.36192 6.3932 5.05493 7.67588C4.91491 8.26082 4.89657 8.62871 4.86607 11.4675L4.83233 14.6113H4.02428C3.54232 14.6113 3.03726 14.6584 2.77262 14.728C1.47068 15.0704 0.467884 16.0862 0.126925 17.4078C-0.0423084 18.0637 -0.0423084 35.5473 0.126925 36.2032C0.467884 37.5248 1.47068 38.5406 2.77262 38.8831C3.16143 38.9853 4.61215 38.9997 14.5071 38.9997C26.9329 38.9997 26.2011 39.0269 27.1226 38.531C27.9896 38.0645 28.7362 37.0296 28.9229 36.0357C28.9725 35.7714 28.9999 32.4635 28.9999 26.735C28.9999 17.0599 29.0163 17.4527 28.5766 16.5767C28.307 16.0393 27.6514 15.3646 27.1226 15.08C26.4828 14.7357 25.917 14.6113 24.9899 14.6113H24.1819L24.1482 11.4675C24.1177 8.62871 24.0993 8.26082 23.9593 7.67588C23.4912 5.72039 22.6774 4.24314 21.3104 2.86717C19.9085 1.45623 18.4433 0.635788 16.5452 0.198854C15.915 0.0538195 14.0457 -0.0507457 13.4503 0.0257729ZM15.7921 3.36355C16.5027 3.51568 17.415 3.89027 17.9723 4.25884C19.4393 5.22896 20.5323 6.86489 20.8414 8.55264C20.8907 8.82175 20.9232 10.1157 20.9232 11.8054V14.6113H14.5071H8.09103L8.09231 11.7724C8.09374 8.67215 8.11918 8.41759 8.53524 7.33109C9.29212 5.35464 11.0723 3.83783 13.1862 3.3682C13.7874 3.23468 15.1783 3.23216 15.7921 3.36355Z"
                            fill="black" />
                        <path fill-rule="evenodd" clip-rule="evenodd"
                            d="M13.4503 0.0257729C11.8822 0.227358 10.3806 0.793779 9.07231 1.67732C8.34495 2.1686 7.01456 3.51187 6.52799 4.24626C5.83437 5.29329 5.36192 6.3932 5.05493 7.67588C4.91491 8.26082 4.89657 8.62871 4.86607 11.4675L4.83233 14.6113H4.02428C3.54232 14.6113 3.03726 14.6584 2.77262 14.728C1.47068 15.0704 0.467884 16.0862 0.126925 17.4078C-0.0423084 18.0637 -0.0423084 35.5473 0.126925 36.2032C0.467884 37.5248 1.47068 38.5406 2.77262 38.8831C3.16143 38.9853 4.61215 38.9997 14.5071 38.9997C26.9329 38.9997 26.2011 39.0269 27.1226 38.531C27.9896 38.0645 28.7362 37.0296 28.9229 36.0357C28.9725 35.7714 28.9999 32.4635 28.9999 26.735C28.9999 17.0599 29.0163 17.4527 28.5766 16.5767C28.307 16.0393 27.6514 15.3646 27.1226 15.08C26.4828 14.7357 25.917 14.6113 24.9899 14.6113H24.1819L24.1482 11.4675C24.1177 8.62871 24.0993 8.26082 23.9593 7.67588C23.4912 5.72039 22.6774 4.24314 21.3104 2.86717C19.9085 1.45623 18.4433 0.635788 16.5452 0.198854C15.915 0.0538195 14.0457 -0.0507457 13.4503 0.0257729ZM15.7921 3.36355C16.5027 3.51568 17.415 3.89027 17.9723 4.25884C19.4393 5.22896 20.5323 6.86489 20.8414 8.55264C20.8907 8.82175 20.9232 10.1157 20.9232 11.8054V14.6113H14.5071H8.09103L8.09231 11.7724C8.09374 8.67215 8.11918 8.41759 8.53524 7.33109C9.29212 5.35464 11.0723 3.83783 13.1862 3.3682C13.7874 3.23468 15.1783 3.23216 15.7921 3.36355Z"
                            fill="#301934 " />
                    </svg>
                    <div class="slot-block-header__content">
                        <h2>You don't have a backpack</h2>
                        <p>Having a 3rd level backpack, they will become <br />
                            All slots are available</p>
                    </div>
                </div>
            </div> -->
        </div>

        <div class="list" v-if="type === 'car'" style="width: 40vh; overflow-y: auto; height: 68.52vh;">
            <div class="list-item" :id="canDrop ? 'drop' : 'nodrop'" v-for="(item, index) in items" :key="index"
                :adress="adress(index)"
                :class="{'list-active': isActive(index), 'list-with-item': item && !item.promo, 'list-with-promo': item && item.promo}"
                @mousedown="onMouseDown(index, item, $event)" @mousemove="onMouseMoveX(index, item, $event)">
                <img v-if="item" :src="item.getImage()" alt="item">
                <div class="list-item-weight" v-if="item && item.count > 1">{{item.count}} </div>
                <!-- <div class="list-item-promo" v-if="item && item.promo"></div> -->
            </div>
        </div>

        <div class="list" v-if="type === 'near'" :style="{'width': width == undefined ? '96%' : `auto`}">
            <div class="list-item" :id="canDrop ? 'drop' : 'nodrop'" v-for="(item, index) in items" :key="index"
                :adress="adress(index)"
                :class="[{
                    'list-active': isActive(index),
                }, getHighlightClassByType(item)]"
                @mousedown="onMouseDown(index, item, $event)">
                <img v-if="item" :src="item.getImage()" alt="item">
                <div class="list-item-weight" v-if="item && item.count > 1">{{item.count}} </div>
                <!-- <div class="list-item-promo" v-if="item && item.promo"></div> -->
            </div>
        </div>
    </div>
</template>

<script>
import dragObject from './classes/dargObject'
import { mapGetters, mapState } from 'vuex'

export default {
    props: ["id", "drag", "width", "sortByIndex", "canDrop", "checkInventory", "type"],
    data() {
        return {
            lastClick: 0,
            clickDelay: 50,
            backpack: false
        }
    },
    computed: {
        ...mapGetters('inventory', ['getById']),
        ...mapState({ dynamicType: (state) => state.inventory.dynamicType }),
        inventory() {
            if (this.checkInventory) return this.checkInventory;

            else return this.getById(this.id);
        },
        items(){
            const items = [];
            const size = Math.max(this.inventory.size, this.inventory.items.length);
            for (let i = 0; i < size; i++) {
                if(this.sortByIndex){
                    const index = this.inventory.items.findIndex(item=>item.index == i);
                    items.push(index === -1 ? null : this.inventory.items[index]);
                }else{
                    items.push(this.inventory.items[i] == undefined ? null : this.inventory.items[i]);
                }
            }
            return items;
        }
    },
    methods: {
        getHighlightClassByType (item) {
            return item ? 'list-item-type-'+ item.config.Type : ''
        },
        adress(index) {
            return `inv_${this.id}_${index}`
        },
        onMouseMoveX(index, item, e) {
            const adress = this.adress(index);

            this.$store.commit('inventory/setSelectedObjectX', [adress, item, e]);
        },
        isActive(index) {
            return this.drag && this.adress(index) == this.drag.overAdress
        },
        onMouseDown(index, item, e) {
            if (item == null) return;
            var adress = this.adress(index);
            if (e.button === 2) {
                if (item.isUsable() || item.isWeapon() || item.isMask()) {
                    this.$emit("onDoubleClick", adress, item)
                }
            } else if (this.lastClick > Date.now()) {
                if (item.isUsable())
                    this.$emit("onDoubleClick", adress, item)
            } else {
                const object = new dragObject(adress, item, e.clientX, e.clientY);
                this.$emit("onMouseDown", object)
            }

            this.lastClick = Date.now() + this.clickDelay;
        },
    }
}
</script>

<style lang="scss" scoped>
.slot-block:hover {
    .block__img {
        animation: pulse 1.5s ease-in-out;

        svg {
            opacity: 1 !important;

            path {
                fill: #301934  !important;
            }
        }
    }
}

.blocked-slots {
    display: flex;
    flex-wrap: wrap;
    justify-content: space-between;
    margin: auto;
    width: auto;
    position: relative;

    .slot-block {
        transition: .5s all;

        &:hover {
            border: 1px solid rgb(255, 255, 255, .2) !important;
        }

        &-header {
            position: absolute;
            left: 0;
            right: 0;
            top: 18vh;
            margin: 0 auto;
            display: flex;
            height: fit-content;
            width: fit-content;
            justify-content: center;
            align-items: center;
            gap: 1vh;

            h2 {
                font-weight: 500;
                font-size: 2.6vh;
                text-transform: uppercase;
                color: #301934 ;
                margin-bottom: 1vh;
            }

            p {
                font-weight: 200;
                font-size: 1.6vh;
                line-height: 135%;
                text-transform: uppercase;

                color: rgba(255, 255, 255, 0.25);
            }
        }

        .block__img {
            color: #fff;
            position: absolute;
            top: -.6vh;
            transition: .5s all;
            font-size: 0.9vh;
            line-height: 1.2vh;
            letter-spacing: 0.03vh;
            right: .4rem !important;

            svg {
                opacity: .35;

                path {
                    fill: white;
                }
            }
        }
    }

    .opacity-60 {
        opacity: .3;
    }
}

@keyframes pulse {
    0% {
        transform: scaleX(1) scaleY(1);
    }

    10% {
        transform: scaleX(0.85) scaleY(0.85);
    }

    20% {
        transform: scaleX(1) scaleY(1);
    }

    30% {
        transform: scaleX(0.85) scaleY(0.85);
    }

    40% {
        transform: scaleX(1) scaleY(1);
    }

    50% {
        transform: scaleX(0.85) scaleY(0.85);
    }

    60% {
        transform: scaleX(1) scaleY(1);
    }

    70% {
        transform: scaleX(0.85) scaleY(0.85);
    }

    80% {
        transform: scaleX(1) scaleY(1);
    }

    90% {
        transform: scaleX(0.85) scaleY(0.85);
    }

    100% {
        transform: scaleX(1) scaleY(1);
    }
}

.list {
    display: flex;
    flex-wrap: wrap;
    justify-content: space-between;
    margin: auto;

    &::-webkit-scrollbar {
        width: 5px;
    }
    &::-webkit-scrollbar-track {
        background: rgba(0, 0, 0, 0.25);
    }
    &::-webkit-scrollbar-thumb {
        background: #301934 ;
    }

    &-item {
        width: 8vh;
        height: 8vh;
        margin: 0 1rem 1rem 0;
        box-sizing: border-box;
        display: flex;
        align-items: center;
        justify-content: center;
        position: relative;

        background: radial-gradient(50% 50% at 50% 50%, rgba(0, 0, 0, 0.05) 0%, rgba(0, 0, 0, 0.09) 100%);
        border: 1px solid rgba(255, 255, 255, 0.1);
        border-radius: 2px;
        transition: .5s all;

        &:hover {
            border: 1px solid rgba(255, 255, 255, 1);
        }

        img {
            width: 70%;
            height: 70%;
            -webkit-user-drag: none;
            -o-user-drag: none;
            pointer-events: none;
        }

        &-weight {
            color: #fff;
            position: absolute;
            top: 0.463vh;
            left: 0.741vh;
            font-size: .9vh;
            line-height: 1.2vh;
            letter-spacing: .03vh;
            font-family: 'Akrobat';
            font-weight: 600;
            font-size: 1.111vh;
            line-height: 1.296vh;
        }
    }

    &.bagStyle, &.bagStyle .blocked-slots {
        gap: 20px 10px;
        .list-item {
            width: 80px;
            height: 80px;
            margin: 0;
        }
    }

    &-item-type-1 {
        background: radial-gradient(50% 50% at 50% 50%, rgba(115, 92, 255, 0.0275) 0%, rgba(115, 92, 255, 0.088) 100%);
        border: 1px solid #735CFF;
    }

    &-item-type-2 {
        border: 1px solid rgba(255, 127, 55, 0.55);
        background: radial-gradient(50% 50% at 50% 50%, rgba(255, 127, 55, 0.0275) 0%, rgba(255, 127, 55, 0.088) 100%);
    }

    

    &-active {
        border: 2px solid rgba(255, 255, 255, 0.6);

    }

    &-with-item {
        background: radial-gradient(80.56% 80.56% at 50% 76.11%, rgba(255, 255, 255, 0.15) 0%, rgba(255, 255, 255, 0) 100%);
    }

    &-with-promo {
        background: radial-gradient(80.56% 80.56% at 50% 76.11%, rgba(182, 211, 0, 0.2) 0%, rgba(182, 211, 0, 0) 100%);
    }
}
</style>