// packages
import { useQuery } from 'react-query';
import { useParams } from 'react-router-dom';

// source
import { ItemResponse } from 'src/models/common/itemResponse';
import { ItemTelemetryDto } from 'src/models/metrics/telemetryDto';
import { getTelemetryItem } from 'src/modules/telemetry/api/api';

const useView = () => {
    const { id } = useParams<{ id: string }>();

    const { data: result } = useQuery<ItemResponse<ItemTelemetryDto>, Error>(
        ['telemetry', id],
        () => getTelemetryItem(id!),
        {
            suspense: true, 
            enabled: !!id 
        }
    );

    return { telemetry: result?.data! };
};

export default useView;
