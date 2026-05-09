  <template>
    <div class="hud-killog" v-if="isKillog">
        <div class="hud-killog-item" v-for="(item, index) in killogItems" :key="index">
            <div class="item-player item-player-left" :class="{ 'item-player-bg': item.me === 'killer' }">
                <span>{{ item.killerName }}</span>
            </div>
            <img
                src="/img/hud/killog/gun.png"
                alt="kill"
                class="hud-killog-center"
            >
            <div class="item-player item-player-right" :class="{ 'item-player-bg': item.me === 'player' }">
                <span>{{ item.playerName }}</span>
            </div>
        </div>
    </div>
</template>

<script>
import {mapState} from 'vuex'

export default {
    computed: {
        ...mapState('hud', ['isKillog', 'killogItems'])
    },
}
</script>

<style lang="scss" scoped>
.hud-killog {
    display: flex;
    flex-direction: column;
    position: fixed;
    overflow: hidden;
    max-height: 12rem;
    gap: 1rem;
    top: 15rem;
    right: 0.5rem;
    .hud-killog-item {
        display: flex;
        align-items: center;
        gap: 1.1rem;
        // margin: 0.45rem 0 0 0;
        .item-player {
            display: flex;
            align-items: center;
            background: transparent;
            border-radius: 1rem;
            padding: 0.3rem 0.5rem;
            span {
                min-width: 6.75rem;
                font-family: 'Akrobat';
                font-weight: 700;
                font-size: 0.8rem;
                line-height: 0.95rem;
                text-transform: uppercase;

                color: #FFFFFF;
            }
        }
        .item-player-bg {
            background: rgba(34, 34, 34, 0.7);
        }
        .item-player img {
            width: 1rem;
        }
    }
    .hud-killog-center {
        width: 5rem;
        height: 1.4rem;
    }
}
 
</style>