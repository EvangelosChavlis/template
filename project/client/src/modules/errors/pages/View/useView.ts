// packages
import { useQuery } from 'react-query';
import { useParams } from 'react-router-dom';

// source
import { ItemResponse } from 'src/models/common/itemResponse';
import { ItemErrorDto } from 'src/models/metrics/errorsDto';
import { getError } from 'src/modules/errors/api/api';

const useView = () => {
    const { id } = useParams<{ id: string }>();

    const { data: result } = useQuery<ItemResponse<ItemErrorDto>, Error>(
        ['role', id],
        () => getError(id!),
        {
            suspense: true, 
            enabled: !!id 
        }
    );

    return { error: result?.data! };
};

export default useView;
